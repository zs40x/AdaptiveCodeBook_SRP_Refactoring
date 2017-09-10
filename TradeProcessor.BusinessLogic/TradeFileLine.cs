using System.Collections.Generic;

namespace TradeProcessor.BusinessLogic
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

        public TradeRecord AsTradeRecord()
        {
            var columns = LineColumns();

            if (columns.Count < 3)
            {
                throw new InvalidTraceFileLineException(
                    $"WARN: Line {_lineNo} malformed. Only {1} field(s) found.");
            }

            if (columns[0].Length != 6)
            {
                throw new InvalidTraceFileLineException(
                    $"WARN: Trade currencies on line {_lineNo} malformed: '{columns[0]}'");
            }

            int tradeAmount;
            if (!int.TryParse(columns[1], out tradeAmount))
            {
                throw new InvalidTraceFileLineException(
                    $"WARN: Trade amount on line {_lineNo} not a valid integer: '{columns[1]}'");
            }

            decimal tradePrice;
            if (!decimal.TryParse(columns[2], out tradePrice))
            {
                throw new InvalidTraceFileLineException(
                    $"WARN: Trade price on line {_fileLine} not a valid decimal: '{columns[2]}'");
            }

            return new TradeRecord
            {
                DestinationCurrency = columns[0].Substring(3,3),
                Lots = tradeAmount / LotSize,
                Price = tradePrice,
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
