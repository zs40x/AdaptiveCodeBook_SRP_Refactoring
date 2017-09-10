using System.Collections.Generic;

namespace TradeProcessor.BusinessLogic
{
    public class TradeFileLine
    {
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

            return null;
        }

        private List<string> LineColumns()
        {
            return string.IsNullOrEmpty(_fileLine) 
                ? new List<string>() 
                : new List<string>(_fileLine.Split(',')) ;
        }
    }
}
