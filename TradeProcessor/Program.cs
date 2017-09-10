using System;
using System.Reflection;

namespace TradeProcessor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("TradeProcessor.ConsoleApp.trades.txt");

            var tradeProcessor = new ConsoleApp.TradeProcessor();
            tradeProcessor.ProcessTrades(tradeStream);

            Console.ReadKey();
        }
    }
}
