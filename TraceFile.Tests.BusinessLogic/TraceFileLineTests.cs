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
    }
}
