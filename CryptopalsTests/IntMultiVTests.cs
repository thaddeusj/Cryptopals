using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cryptopals;

namespace CryptopalsTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class IntMultiVTests
    {
        public IntMultiVTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void hammingdistTest()
        {
            Assert.IsTrue(cryptopals.IntMultiVigenere.hammingDist("this is a test", "wokka wokka!!!") == 37);
        }

        [TestMethod]
        public void ihammingTest()
        {
            string hex1 = "7468697320697320612074657374";
            string hex2 = "776f6b6b6120776f6b6b61212121";

            Assert.IsTrue(IntMultiVigenere.ihammingDist(IntConversion.hexToInts(hex1), IntConversion.hexToInts(hex2)) == 37);
        }
    }
}
