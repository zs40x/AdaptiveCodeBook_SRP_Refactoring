using System.Linq;

namespace TradeProcessor.ConsoleApp
{
    internal class TradeProcessor
    {

        private readonly TradeFile _tradeFile;
        private readonly TradeDatabase _tradeDatabase;

        public TradeProcessor(TradeFile tradeFile, TradeDatabase tradeDatabase)
        {
            _tradeFile = tradeFile;
            _tradeDatabase = tradeDatabase;
        }

        public void ProcessTrades()
        {
            var tradeLines = _tradeFile.TradeLines();

            var tradeRecords = 
                tradeLines
                .Select(tradeLine => tradeLine.AsTradeRecord())
                .ToList();

            _tradeDatabase.InsertTradeRecords(tradeRecords);

            System.Console.WriteLine("INFO: {0} trades processed", tradeRecords.Count);
        }
    }
}