using System.Collections.Generic;
using TradeProcessor.BusinessLogic;

namespace TradeProcessor.Core.Domain
{
    public class TradeFileLine
    {
        private static float LotSize = 100000f;

        private readonly int _lineNo;
        private readonly string _fileLine;

        public TradeFileLine(int lineNo, string fileLine)
        {
            _fileLine = fileLine;
            _lineNo = lineNo;
        }

        public TradeLineValidationResult Validate()
        {
            // ToDo: Extract this to the businessLogic project

            var logMessages = new List<string>();
            var columns = LineColumns();

            if (string.IsNullOrEmpty(_fileLine))
            {
                return new TradeLineValidationResult(false, new List<string>());
            }

            if (columns.Count < 3)
            {
                return new TradeLineValidationResult(false, new List<string> { $"WARN: Line {_lineNo} malformed. Only {1} field(s) found." });
            }

            if (columns[0].Length != 6)
            {
                logMessages.Add($"WARN: Trade currencies on line {_lineNo} malformed: '{columns[0]}'");
            }

            if (!int.TryParse(columns[1], out _))
            {
                logMessages.Add($"WARN: Trade amount on line {_lineNo} not a valid integer: '{columns[1]}'");
            }

            if (!decimal.TryParse(columns[2], out _))
            {
                logMessages.Add($"WARN: Trade price on line {_lineNo} not a valid decimal: '{columns[2]}'");
            }

            return new TradeLineValidationResult(true, logMessages);
        }

        public TradeRecord AsTradeRecord()
        {
            var columns = LineColumns();

            return new TradeRecord
            {
                DestinationCurrency = columns[0].Substring(3,3),
                Lots = int.Parse(columns[1]) / LotSize,
                Price = decimal.Parse(columns[2]),
                SourceCurrency = columns[0].Substring(0,3)
            };
        }

        private List<string> LineColumns()
        {
            return string.IsNullOrEmpty(_fileLine) 
                ? new List<string>() 
                : new List<string>(_fileLine.Split(',')) ;
        }
    }
}
