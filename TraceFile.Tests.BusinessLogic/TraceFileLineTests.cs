using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeProcessor.BusinessLogic;

namespace TraceFile.Tests.BusinessLogic
{
    [TestClass]
    public class TraceFileLineTests
    {
        [TestMethod]
        public void InitialLine()
        {
            var validationResult = MakeTestInstance("test").Validate();
            Assert.IsFalse(validationResult.Processed,"Should not be marked as processed");
            Assert.AreEqual("WARN: Line 1 malformed. Only 1 field(s) found.",validationResult.LogMessages.First());
        }

        [TestMethod]
        public void InvaildTradeCurrency()
        {
            var validationResult = MakeTestInstance("abc,100,1").Validate();
            Assert.IsTrue(validationResult.Processed, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade currencies on line 1 malformed: 'abc'", validationResult.LogMessages.First());
        }

        [TestMethod]
        public void TradeAmountNotAValidInteger()
        {
            var validationResult = MakeTestInstance("GPBUSD,xyz,1").Validate();
            Assert.IsTrue(validationResult.Processed, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade amount on line 1 not a valid integer: 'xyz'", validationResult.LogMessages.First());
        }

        [TestMethod]
        public void TradePriceNotAValidDecimal()
        {
            var validationResult = MakeTestInstance("GPBUSD,100,mki").Validate();
            Assert.IsTrue(validationResult.Processed, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade price on line 1 not a valid decimal: 'mki'", validationResult.LogMessages.First());
        }

        [TestMethod]
        public void TradeAmoundAndPriceInvalid()
        {
            var validationResult = MakeTestInstance("GBPUSD,afd,zds").Validate();
            Assert.IsTrue(validationResult.Processed, "Should be marked as processed");
            Assert.IsTrue(validationResult.LogMessages.Contains("WARN: Trade amount on line 1 not a valid integer: 'afd'"));
            Assert.IsTrue(validationResult.LogMessages.Contains("WARN: Trade price on line 1 not a valid decimal: 'zds'"));
        }

        [TestMethod]
        public void ReturnsTradeRecord()
        {
            var tradeRecord = new TradeFileLine(1,"GPBUSD,100,0.2").AsTradeRecord();

            Assert.IsNotNull(tradeRecord);
            Assert.AreEqual("GPB", tradeRecord.SourceCurrency);
            Assert.AreEqual("USD",tradeRecord.DestinationCurrency);
            Assert.AreEqual(0.001f, tradeRecord.Lots);
            Assert.AreEqual(0.2m,tradeRecord.Price);
        }

        private static TradeFileLine MakeTestInstance(string tradeLine)
        {
            return new TradeFileLine(1, tradeLine);
        }
    }
}
