using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace TradeProcessor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = new FileStream("trades.txt",FileMode.Open);

            var tradeProcessor = 
                new ConsoleApp.TradeProcessor(
                    new TradeFile(tradeStream),
                    new TradeDatabase(new SqlConnection("Data Source=(local);Initial Catalog=Trades;Integrated Security=True;")));
            tradeProcessor.ProcessTrades();

            Console.ReadKey();
        }
    }
}
