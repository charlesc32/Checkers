using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CheckersTests
{
    
    
    /// <summary>
    ///This is a test class for GameManagerTest and is intended
    ///to contain all GameManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameManagerTest
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
        ///A test for GameManager Draw game
        ///</summary>
        [TestMethod()]
        public void GameStopsForDrawTest()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board board = TestHelper.CreateEmptyBoard();
            board[3][0] = "X";
            board[3][2] = "X";
            board[3][4] = "X";
            board[3][6] = "X";
            board[4][1] = "O";
            board[4][3] = "O";
            board[4][5] = "O";
            board[4][7] = "O";

            GameManager target = new GameManager(whitePlayer, blackPlayer, board);

            Assert.IsTrue(target.IsDraw);
        }

        /// <summary>
        ///A test for GameManager Win Game
        ///</summary>
        [TestMethod()]
        public void GameStopsForWinTest()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board board = TestHelper.CreateEmptyBoard();
            board[3][6] = "X";
            board[4][1] = "O";
            board[4][3] = "O";
            board[4][5] = "O";
            board[4][7] = "O";

            GameManager target = new GameManager(whitePlayer, blackPlayer, board);

            Assert.IsTrue(target.Winner == "White");
        }

        /// <summary>
        ///A test for GameManager full game vs 2 computers
        ///</summary>
        [TestMethod()]
        public void FullGame()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board board = new Board();
            
            GameManager target = new GameManager(whitePlayer, blackPlayer, board);

            Assert.Inconclusive();
        }
    }
}
