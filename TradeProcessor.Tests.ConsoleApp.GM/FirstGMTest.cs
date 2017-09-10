using System;
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

        [TestInitialize]
        public void Setup()
        {
            _applicationUnderTest = new ApplicationUnderTest(ApplicationFile);
        }

        [TestMethod]
        public void TestMethod1()
        {
            _applicationUnderTest.Run();

            Assert.AreEqual("", _applicationUnderTest.ConsoleOutput);
        }
    }
}