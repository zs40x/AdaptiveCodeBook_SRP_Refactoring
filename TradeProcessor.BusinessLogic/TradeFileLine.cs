namespace TradeProcessor.BusinessLogic
{
    public class TradeFileLine
    {
        private int _lineNo;
        private string _fileLine;

        public TradeFileLine(int lineNo, string fileLine)
        {
            _fileLine = fileLine;
            _lineNo = lineNo;
        }

        public TradeRecord AsTradeRecord()
        {
            if (string.IsNullOrEmpty(_fileLine))
            {
                throw new InvalidTraceFileLineException(
                        string.Format("WARN: Line {0} malformed. Only {1} field(s) found.", _lineNo, 1)
                    );
            }
            return null;
        }
    }
}
