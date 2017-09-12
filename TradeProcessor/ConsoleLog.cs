using System;

namespace TradeProcessor.ConsoleApp
{
    internal interface ILog
    {
        void Log(string message);
    }

    internal class ConsoleLog : ILog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
