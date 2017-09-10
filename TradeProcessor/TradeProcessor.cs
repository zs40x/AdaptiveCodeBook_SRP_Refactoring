using System;
using System.Collections.Generic;
using System.Linq;
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
            var tradeRecords = new List<TradeRecord>();

            foreach (var tradeLine in _tradeFile.TradeLines())
            {
                try
                {
                    tradeRecords.Add(tradeLine.AsTradeRecord());
                }
                catch (InvalidTraceFileLineException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            _tradeDatabase.InsertTradeRecords(tradeRecords);

            Console.WriteLine("INFO: {0} trades processed", tradeRecords.Count);
        }
    }
}