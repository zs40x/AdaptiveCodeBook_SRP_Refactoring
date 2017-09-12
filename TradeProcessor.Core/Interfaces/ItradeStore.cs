using System.Collections.Generic;
using TradeProcessor.Core.Domain;

namespace TradeProcessor.Core.Interfaces
{
    public interface ITradeStore
    {
        void InsertTradeRecords(List<TradeRecord> tradeRecords);
    }
}
