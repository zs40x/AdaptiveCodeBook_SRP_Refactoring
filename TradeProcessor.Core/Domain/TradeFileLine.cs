using System.Collections.Generic;
using TradeProcessor.BusinessLogic;

namespace TradeProcessor.Core.Domain
{
    public class TradeFileLine
    {
        private static float LotSize = 100000f;
        
        public int LineNo { get; }

        public string FileLine { get; }

        public List<string> LineColumns => string.IsNullOrEmpty(FileLine)
            ? new List<string>()
            : new List<string>(FileLine.Split(','));


        public TradeFileLine(int lineNo, string fileLine)
        {
            FileLine = fileLine;
            LineNo = lineNo;
        }

        public TradeRecord AsTradeRecord()
        {
            var columns = LineColumns;

            return new TradeRecord
            {
                DestinationCurrency = columns[0].Substring(3,3),
                Lots = int.Parse(columns[1]) / LotSize,
                Price = decimal.Parse(columns[2]),
                SourceCurrency = columns[0].Substring(0,3)
            };
        }
    }
}
