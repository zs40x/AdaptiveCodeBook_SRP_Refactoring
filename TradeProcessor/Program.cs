using System;
using System.IO;
using System.Reflection;

namespace TradeProcessor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = new FileStream("trades.txt",FileMode.Open);

            var tradeProcessor = new ConsoleApp.TradeProcessor();
            tradeProcessor.ProcessTrades(tradeStream);

            Console.ReadKey();
        }
    }
}
