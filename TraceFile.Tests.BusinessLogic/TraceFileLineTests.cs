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
    }
}
