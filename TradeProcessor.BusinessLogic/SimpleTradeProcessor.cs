using System;
using System.Collections.Generic;
using System.Linq;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.BusinessLogic
{
    public class SimpleTradeProcessor
    {
        private readonly ITradeFilesystem _tradeFile;
        private readonly ITradeStore _tradeStore;
        private readonly ILog _log;
        private readonly SimpleTradeValidator _tradeValidator;

        public SimpleTradeProcessor(ITradeFilesystem tradeFile, ITradeStore tradeStore, ILog log)
        {
            _tradeFile = tradeFile;
            _tradeStore = tradeStore;
            _log = log;
            _tradeValidator = new SimpleTradeValidator(_log);
        }

        public void ProcessTrades()
        {
            var tradeRecords =
                _tradeFile.FileContent()
                    .Where(tradeLine => _tradeValidator.Validate(tradeLine))
                    .Select(tradeLine => tradeLine.AsTradeRecord())
                    .ToList();

            _tradeStore.InsertTradeRecords(tradeRecords);

            _log.Log($"INFO: {tradeRecords.Count} trades processed");
        }
    }
}