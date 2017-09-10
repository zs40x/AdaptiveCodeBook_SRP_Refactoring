using System.Collections.Generic;

namespace TradeProcessor.ConsoleApp
{
    internal class TradeProcessor
    {

        private static float LotSize = 100000f;

        private readonly TradeFile _tradeFile;
        private readonly TradeDatabase _tradeDatabase;

        public TradeProcessor(TradeFile tradeFile, TradeDatabase tradeDatabase)
        {
            _tradeFile = tradeFile;
            _tradeDatabase = tradeDatabase;
        }

        public void ProcessTrades()
        {
            var trades = new List<TradeRecord>();

            var lineCount = 1;
            foreach (var line in _tradeFile.TradeLines())
            {
                var fields = line.Split(new char[] { ',' });

                if (fields.Length != 3)
                {
                    System.Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);
                    continue;
                }

                if (fields[0].Length != 6)
                {
                    System.Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);
                    continue;
                }

                int tradeAmount;
                if (!int.TryParse(fields[1], out tradeAmount))
                {
                    System.Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);
                }

                decimal tradePrice;
                if (!decimal.TryParse(fields[2], out tradePrice))
                {
                    System.Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);
                }

                var sourceCurrencyCode = fields[0].Substring(0, 3);
                var destinationCurrencyCode = fields[0].Substring(3, 3);

                // calculate values
                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LotSize,
                    Price = tradePrice
                };

                trades.Add(trade);

                lineCount++;
            }

            _tradeDatabase.InsertTradeRecords(trades);

            System.Console.WriteLine("INFO: {0} trades processed", trades.Count);
        }


        
    }
}