using System;
using System.Collections.Generic;
using TradeProcessor.BusinessLogic;
using TradeProcessor.Core;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.ConsoleApp
{
    internal class TradeProcessor
    {
        private readonly TradeFile _tradeFile;
        private readonly ITradeStore _tradeStore;
        private readonly ILog _log;

        public TradeProcessor(TradeFile tradeFile, ITradeStore tradeStore, ILog log)
        {
            _tradeFile = tradeFile;
            _tradeStore = tradeStore;
            _log = log;
        }

        public void ProcessTrades()
        {
            var processedCount = 0;
            var tradeLines = _tradeFile.TradeLines() as IList<TradeFileLine> ?? new List<TradeFileLine>();
            var tradeRecords = new List<TradeRecord>();

            foreach (var tradeLine in tradeLines)
            {
                var validationResult = tradeLine.Validate();

                validationResult.LogMessages.ForEach(_log.Log);

                if(!validationResult.Processed) continue;
               
                tradeRecords.Add(tradeLine.AsTradeRecord());
                processedCount += 1;
            }

            _tradeStore.InsertTradeRecords(tradeRecords);

            _log.Log($"INFO: {processedCount} trades processed");
        }
    }
}