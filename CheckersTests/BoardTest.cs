using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckersTests
{
    
    
    /// <summary>
    ///This is a test class for BoardTest and is intended
    ///to contain all BoardTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BoardTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Board Constructor
        ///</summary>
        [TestMethod()]
        public void BoardConstructorTest()
        {
            Board target = new Board();
            string actual = target.ToString();
            string expected = "  0 1 2 3 4 5 6 7" + Environment.NewLine +
                            "0 . X . X . X . X" + Environment.NewLine +
                            "1 X . X . X . X ." + Environment.NewLine +
                            "2 . X . X . X . X" + Environment.NewLine +
                            "3 . . . . . . . ." + Environment.NewLine +
                            "4 . . . . . . . ." + Environment.NewLine +
                            "5 O . O . O . O ." + Environment.NewLine +
                            "6 . O . O . O . O" + Environment.NewLine +
                            "7 O . O . O . O ." + Environment.NewLine;

            Assert.AreEqual(expected, actual);
        }
    }
}
