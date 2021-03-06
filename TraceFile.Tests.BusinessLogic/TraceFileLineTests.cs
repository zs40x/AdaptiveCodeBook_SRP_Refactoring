﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeProcessor.BusinessLogic;
using TradeProcessor.Core.Domain;

namespace TraceFile.Tests.BusinessLogic
{
    [TestClass]
    public class TraceFileLineTests
    {
        private MockLog _mockLog;

        [TestInitialize]
        public void SetUp()
        {
            _mockLog = new MockLog();
        }

        [TestMethod]
        public void InitialLine()
        {
            var validationResult = Validate("test");
            Assert.IsFalse(validationResult,"Should not be marked as processed");
            Assert.AreEqual("WARN: Line 1 malformed. Only 1 field(s) found.",_mockLog.LoggedMessages.First());
        }

        [TestMethod]
        public void InvaildTradeCurrency()
        {
            var validationResult = Validate("abc,100,1");
            Assert.IsTrue(validationResult, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade currencies on line 1 malformed: 'abc'", _mockLog.LoggedMessages.First());
        }

        [TestMethod]
        public void TradeAmountNotAValidInteger()
        {
            var validationResult = Validate("GPBUSD,xyz,1");
            Assert.IsTrue(validationResult, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade amount on line 1 not a valid integer: 'xyz'", _mockLog.LoggedMessages.First());
        }

        [TestMethod]
        public void TradePriceNotAValidDecimal()
        {
            var validationResult = Validate("GPBUSD,100,mki");
            Assert.IsTrue(validationResult, "Should be marked as processed");
            Assert.AreEqual("WARN: Trade price on line 1 not a valid decimal: 'mki'", _mockLog.LoggedMessages.First());
        }

        [TestMethod]
        public void TradeAmoundAndPriceInvalid()
        {
            var validationResult = Validate("GBPUSD,afd,zds");
            Assert.IsTrue(validationResult, "Should be marked as processed");
            Assert.IsTrue(_mockLog.LoggedMessages.Contains("WARN: Trade amount on line 1 not a valid integer: 'afd'"));
            Assert.IsTrue(_mockLog.LoggedMessages.Contains("WARN: Trade price on line 1 not a valid decimal: 'zds'"));
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

        private bool Validate(string tradeLine)
        {
            return new SimpleTradeValidator(_mockLog)
                    .Validate(new TradeFileLine(1, tradeLine));
        }
    }
}
