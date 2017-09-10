using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeProcessor.ConsoleApp;

namespace TradeProcessor.Tests.ConsoleApp
{
    [TestClass]
    public class TradeFileTest
    {
        [TestMethod]
        public void InitialLine()
        {
            var line = new TradeLine("");
        }
    }
}
