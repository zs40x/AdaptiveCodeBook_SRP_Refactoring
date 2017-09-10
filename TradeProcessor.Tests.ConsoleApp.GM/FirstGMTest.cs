using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TradeProcessor.Tests.ConsoleApp.GM
{
    [TestClass]
    public class FirstGMTest
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
        public void FileWithIncompleteLine()
        {
            _testDataInputFile.WithContent("Test");

            _applicationUnderTest.Run();

            Assert.AreEqual(
                "WARN: Line 1 malformed. Only 1 field(s) found.\r\nINFO: 0 trades processed\r\n",
                _applicationUnderTest.ConsoleOutput);
        }
    }
}