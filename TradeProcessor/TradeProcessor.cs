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
            var tradeLines = _tradeFile.TradeLines() as IList<TradeFileLine> ?? new List<TradeFileLine>();
            var tradeRecords = new List<TradeRecord>();

            foreach (var tradeLine in tradeLines)
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

            Console.WriteLine("INFO: {0} trades processed", tradeLines.Count());
        }
    }
}