using System.Collections.Generic;

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
                Lots = (StringAsNullableInt(columns[1]) ?? 0) / LotSize,
                Price = StringAsNullableDecimal(columns[2]) ?? 0m,
                SourceCurrency = columns[0].Substring(0,3)
            };
        }

        private int? StringAsNullableInt(string str)
        {
            return int.TryParse(str, out var integer) ? (int?)integer : null;
        }

        private decimal? StringAsNullableDecimal(string str)
        {
            return decimal.TryParse(str, out var dec) ? (decimal?) dec : null;
        }
    }
}
