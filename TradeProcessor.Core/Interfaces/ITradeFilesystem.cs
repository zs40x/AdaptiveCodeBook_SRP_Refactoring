﻿using System.Collections.Generic;
using TradeProcessor.Core.Domain;

namespace TradeProcessor.Core.Interfaces
{
    public interface ITradeFilesystem
    {
        List<TradeFileLine> FileContent();
    }
}
