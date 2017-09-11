using System;
using System.Collections.Generic;
using TradeProcessor.BusinessLogic;

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
            var processedCount = 0;
            var tradeLines = _tradeFile.TradeLines() as IList<TradeFileLine> ?? new List<TradeFileLine>();
            var tradeRecords = new List<TradeRecord>();

            foreach (var tradeLine in tradeLines)
            {
                var validationResult = tradeLine.Validate();

                validationResult.LogMessages.ForEach(Console.WriteLine);

                if(!validationResult.Processed) continue;
               
                tradeRecords.Add(tradeLine.AsTradeRecord());
                processedCount += 1;
            }

            _tradeDatabase.InsertTradeRecords(tradeRecords);

            Console.WriteLine($"INFO: {processedCount} trades processed");
        }
    }
}