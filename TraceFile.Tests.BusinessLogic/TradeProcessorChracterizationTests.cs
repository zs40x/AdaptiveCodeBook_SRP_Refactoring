using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeProcessor.BusinessLogic;
using TradeProcessor.Core.Domain;
using TradeProcessor.Core.Interfaces;

namespace TraceFile.Tests.BusinessLogic
{
    class FakeTradeFileSystem : ITradeFilesystem
    {
        readonly List<string> _fileLines = new List<string>();

        public void WithContent(string line)
        {
            _fileLines.Add(line);
        }

        public List<TradeFileLine> FileContent()
        {
            var tradeFileLines = new List<TradeFileLine>();
            var lineNo = 1;

            foreach (var fileLine in _fileLines)
            {
                tradeFileLines.Add(new TradeFileLine(lineNo, fileLine));
                lineNo = lineNo + 1;
            }

            return tradeFileLines;
        }
    }

    class MockLog : ILog
    {
        public List<string> LoggedMessages { get; } = new List<string>();

        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }

    class MockTradeStore : ITradeStore
    {
        public List<TradeRecord> InsertedTradeRecords { get; } = new List<TradeRecord>();

        public void InsertTradeRecords(List<TradeRecord> tradeRecords)
        {
            InsertedTradeRecords.AddRange(tradeRecords);
        }
    }

    [TestClass]
    public class TradeProcessorChracterizationTests
    {
        private FakeTradeFileSystem _tradeFilesystem;
        private MockLog _mockLog;
        private MockTradeStore _mockTradeStore;
        private SimpleTradeProcessor _testInstance;

        [TestInitialize]
        public void Setup()
        {
            _tradeFilesystem = new FakeTradeFileSystem();
            _mockTradeStore = new MockTradeStore();
            _mockLog = new MockLog();

            _testInstance = new SimpleTradeProcessor(_tradeFilesystem, _mockTradeStore, _mockLog);
        }

        [TestMethod]
        public void EmptyInputFile()
        {
            _tradeFilesystem.WithContent(string.Empty);

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string> {"INFO: 0 trades processed"},
                _mockLog.LoggedMessages);
        }

        [TestMethod]
        public void OneFieldOnly()
        {
            _tradeFilesystem.WithContent("Test");

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string>
                {
                    "WARN: Line 1 malformed. Only 1 field(s) found.",
                    "INFO: 0 trades processed"
                },
                _mockLog.LoggedMessages);
        }

        [TestMethod]
        public void MalformedCurrencyPair()
        {
            _tradeFilesystem.WithContent("GBPUSD,123,test");

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string>
                {
                    "WARN: Trade price on line 1 not a valid decimal: \'test\'",
                    "INFO: 1 trades processed"
                },
                _mockLog.LoggedMessages);
        }

        [TestMethod]
        public void TradeVolumeInvalid()
        {
            _tradeFilesystem.WithContent("GBPUSD,xyz,abc");

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string>
                {
                    "WARN: Trade amount on line 1 not a valid integer: \'xyz\'",
                    "WARN: Trade price on line 1 not a valid decimal: \'abc\'",
                    "INFO: 1 trades processed"
                },
                _mockLog.LoggedMessages);
        }

        [TestMethod]
        public void TradeAmountInvalid()
        {
            _tradeFilesystem.WithContent("GBPUSD,100,abc");

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string>
                {
                    "WARN: Trade price on line 1 not a valid decimal: \'abc\'",
                    "INFO: 1 trades processed"
                },
                _mockLog.LoggedMessages);
        }

        [TestMethod]
        public void CorrectFormat()
        {
            _tradeFilesystem.WithContent("GBPUSD,100,0.81");

            _testInstance.ProcessTrades();

            CollectionAssert.AreEquivalent(
                new List<string> {"INFO: 1 trades processed"},
                _mockLog.LoggedMessages);
        }
    }
}
