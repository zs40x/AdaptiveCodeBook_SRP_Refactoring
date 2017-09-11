using System.Collections.Generic;

namespace TradeProcessor.BusinessLogic
{
    public struct TradeLineValidationResult
    {
        public static TradeLineValidationResult Initial = new TradeLineValidationResult(false, new List<string>());

        public bool Processed { get; }
        public List<string> LogMessages { get; }

        public TradeLineValidationResult(bool processed, List<string> logMessages)
        {
            Processed = processed;
            LogMessages = logMessages;
        }
    }
}
