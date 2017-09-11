using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TradeProcessor.Tests.ConsoleApp.GM
{
    [TestClass]
    public class ChracterizationTests
    {
        private const string ApplicationFile = "TradeProcessor.exe";
        private ApplicationUnderTest _applicationUnderTest;
        private TestDataInputFile _testDataInputFile;

        [TestInitialize]
        public void Setup()
        {
            _applicationUnderTest = new ApplicationUnderTest(ApplicationFile);
            _testDataInputFile = new TestDataInputFile("trades.txt");
        }

        [TestMethod]
        public void EmptyInputFile()
        {
            _testDataInputFile.WithContent(string.Empty);

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "INFO: 0 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }

        [TestMethod]
        public void OneFieldOnly()
        {
            _testDataInputFile.WithContent("Test");

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "WARN: Line 1 malformed. Only 1 field(s) found.\r\nINFO: 0 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }

        [TestMethod]
        public void MalformedCurrencyPair()
        {
            _testDataInputFile.WithContent("Test,123,abc");
      
            _applicationUnderTest.Run();

            Assert.AreEqual(
                "WARN: Trade currencies on line 1 malformed: \'Test\'\r\nINFO: 0 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }

        [TestMethod]
        public void TradeVolumeInvalid()
        {
            _testDataInputFile.WithContent("GBPUSD,xyz,0.1");

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "WARN: Trade amount on line 1 not a valid integer: \'xyz\'\r\nWARN: Trade price on line 1 not a valid decimal: \'abc\'\r\nINFO: 1 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }

        [TestMethod]
        public void TradeAmountInvalid()
        {
            _testDataInputFile.WithContent("GBPUSD,100,abc");

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "WARN: Trade price on line 1 not a valid decimal: \'abc\'\r\nINFO: 1 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }

        [TestMethod]
        public void CorrectFormat()
        {
            _testDataInputFile.WithContent("GBPUSD,100,0.81");

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "INFO: 1 trades processed\r\n", 
                _applicationUnderTest.ConsoleOutput);
        }
    }
}