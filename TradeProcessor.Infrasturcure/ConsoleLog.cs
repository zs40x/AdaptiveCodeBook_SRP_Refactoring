using System;
using TradeProcessor.Core.Interfaces;

namespace TradeProcessor.Infrasturcure
{
    public class ConsoleLog : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
