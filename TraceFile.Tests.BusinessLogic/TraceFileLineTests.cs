using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeProcessor.BusinessLogic;

namespace TraceFile.Tests.BusinessLogic
{
    [TestClass]
    public class TraceFileLineTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidTraceFileLineException), "WARN: Line 1 malformed. Only 1 field(s) found.")]
        public void InitialLine()
        {
            new TradeFileLine(1, "").AsTradeRecord();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTraceFileLineException),"WARN: Trade currencies on line 1 malformed: 'abc'")]
        public void InvaildTradeCurrency()
        {
            new TradeFileLine(1, "abc,100,1").AsTradeRecord();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTraceFileLineException), "WARN: Trade amount on line 1 not a valid integer: xyz")]
        public void TradeAmountNotAValidInteger()
        {
            new TradeFileLine(1, "GPBUSD,xyz,1").AsTradeRecord();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTraceFileLineException),"WARN: Trade price on line 1 not a valid decimal: mki")]
        public void TradePriceNotAValidDecimal()
        {
            new TradeFileLine(1, "GPBUSD,100,mki").AsTradeRecord();
        }

        [TestMethod]
        public void ReturnsTradeRecord()
        {
            var tradeRecord = new TradeFileLine(1,"GPBUSD,100,0.2").AsTradeRecord();

            Assert.IsNotNull(tradeRecord);
            Assert.AreEqual("GPB", tradeRecord.SourceCurrency);
            Assert.AreEqual("USD",tradeRecord.DestinationCurrency);
            Assert.AreEqual(100f, tradeRecord.Lots);
            Assert.AreEqual(0.2m,tradeRecord.Price);
        }
    }
}
