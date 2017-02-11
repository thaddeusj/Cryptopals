using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptopalsTests
{
    /// <summary>
    /// Summary description for IntConvTest
    /// </summary>
    [TestClass]
    public class IntConvTest
    {
        public IntConvTest()
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
        public void hexToIntsTest()
        {
            string[] hexs = { "00", "ab", "cf1345", "12345678" };

            int[][] hexConv = new int[hexs.Length][];
            for(int i = 0; i < hexs.Length; i++)
            {
                hexConv[i] = cryptopals.IntConversion.hexToInts(hexs[i]);
            }

            int j = 0;
        }
    }
}
