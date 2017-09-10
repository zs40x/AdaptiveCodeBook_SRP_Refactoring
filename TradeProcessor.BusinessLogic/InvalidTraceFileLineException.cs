using System;

namespace TradeProcessor.BusinessLogic
{
    public class InvalidTraceFileLineException: Exception
    {
        public InvalidTraceFileLineException(string message) : base(message) { }
    }
}
