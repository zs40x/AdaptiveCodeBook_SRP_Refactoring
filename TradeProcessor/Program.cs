using System;
using System.Data.SqlClient;
using System.IO;
using TradeProcessor.Infrasturcure;

namespace TradeProcessor.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = new FileStream("trades.txt",FileMode.Open);

            var tradeProcessor = 
                new TradeProcessor(
                    new TradeFile(tradeStream),
                    new SqlServerTradeStore(new SqlConnection("Data Source=(local);Initial Catalog=Trades;Integrated Security=True;")),
                    new ConsoleLog());

            tradeProcessor.ProcessTrades();

            Console.ReadKey();
        }
    }
}
