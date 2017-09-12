using System;
using System.Collections.Generic;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.BusinessLogic
{
    public class SimpleTradeProcessor
    {
        private readonly ITradeFilesystem _tradeFile;
        private readonly ITradeStore _tradeStore;
        private readonly ILog _log;

        public SimpleTradeProcessor(ITradeFilesystem tradeFile, ITradeStore tradeStore, ILog log)
        {
            _tradeFile = tradeFile;
            _tradeStore = tradeStore;
            _log = log;
        }

        public void ProcessTrades()
        {
            var processedCount = 0;
            var tradeLines = _tradeFile.FileContent() as IList<TradeFileLine>;
            var tradeRecords = new List<TradeRecord>();

            foreach (var tradeLine in tradeLines)
            {
                var validationResult = tradeLine.Validate();

                validationResult.LogMessages.ForEach(_log.Log);

                if(!validationResult.Processed) continue;

                try
                {
                    tradeRecords.Add(tradeLine.AsTradeRecord());
                }
                catch (Exception ex)
                {
                    
                }


                processedCount += 1;
            }

            _tradeStore.InsertTradeRecords(tradeRecords);

            _log.Log($"INFO: {processedCount} trades processed");
        }
    }
}