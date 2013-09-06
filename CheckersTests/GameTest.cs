using Checkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CheckersTests
{
    
    
    /// <summary>
    ///This is a test class for GameTest and is intended
    ///to contain all GameTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameTest
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
        ///A test for GetRemainingPieces
        ///</summary>
        [TestMethod()]
        public void GetRemainingPiecesTest()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board board = new Board();

            Game target = new Game(whitePlayer, blackPlayer, board);
            string color = "White";
            int expected = 12; 
            int actual;
            actual = target.GetRemainingPieces(color);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for DoMove
        ///</summary>
        [TestMethod()]
        public void DoMoveTestForJumpUpdateDownRight()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[2][1] = "O";
            gameBoard[3][2] = "X";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);
            Move move = new Move() { From = new Tuple<int, int>(2, 1), To = new Tuple<int, int>(4, 3), IsJump = true };
            
            target.DoMove(move);

            //Validate that the board is correctly updated after a jump
            Assert.IsTrue(target.GameBoard[3][2] == ".");
            Assert.IsTrue(target.GameBoard[2][1] == ".");
            Assert.IsTrue(target.GameBoard[4][3] == "O");
        }

        [TestMethod()]
        public void DoMoveTestForJumpUpdateDownLeft()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[2][3] = "O";
            gameBoard[3][2] = "X";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);
            Move move = new Move() { From = new Tuple<int, int>(2, 3), To = new Tuple<int, int>(4, 1), IsJump = true };
            
            target.DoMove(move);

            //Validate that the board is correctly updated after a jump
            Assert.IsTrue(target.GameBoard[2][3] == ".");
            Assert.IsTrue(target.GameBoard[3][2] == ".");
            Assert.IsTrue(target.GameBoard[4][1] == "O");
        }

        [TestMethod()]
        public void DoMoveTestForJumpUpdateUpLeft()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();
            
            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[4][3] = "O";
            gameBoard[3][2] = "X";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);
            Move move = new Move() { From = new Tuple<int, int>(4, 3), To = new Tuple<int, int>(2, 1), IsJump = true };
            
            target.DoMove(move);

            //Validate that the board is correctly updated after a jump
            Assert.IsTrue(target.GameBoard[3][2] == ".");
            Assert.IsTrue(target.GameBoard[2][1] == "O");
            Assert.IsTrue(target.GameBoard[4][3] == ".");
        }

        [TestMethod()]
        public void DoMoveTestForJumpUpdateUpRight()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();

            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[4][1] = "O";
            gameBoard[3][2] = "X";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);
            Move move = new Move() { From = new Tuple<int, int>(4, 1), To = new Tuple<int, int>(2, 3), IsJump = true };
            
            target.DoMove(move);

            //Validate that the board is correctly updated after a jump
            Assert.IsTrue(target.GameBoard[3][2] == ".");
            Assert.IsTrue(target.GameBoard[2][3] == "O");
            Assert.IsTrue(target.GameBoard[4][1] == ".");
        }

        /// <summary>
        ///A test for GetValidMoves
        ///</summary>
        [TestMethod()]
        public void GetValidMovesTestORegular()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();

            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[4][1] = "O";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);

            List<Move> actual = target.GetValidMovesRemaining();
            var fromLocation = new Tuple<int, int>(4, 1);

            var expected = new List<Move>() 
            {
                new Move{ From = fromLocation, To = new Tuple<int, int>(3, 0), IsJump = false}, 
                new Move{ From = fromLocation, To = new Tuple<int, int>(3, 2), IsJump = false}
            };
        }

        /// <summary>
        ///A test for GetValidMoves
        ///</summary>
        [TestMethod()]
        public void GetValidMovesTestOJump()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();

            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[2][1] = "X";
            gameBoard[3][2] = "O";

            Game target = new Game(whitePlayer, blackPlayer, gameBoard);

            List<Move> actual = target.GetValidMovesRemaining();
            var fromLocation = new Tuple<int, int>(3, 2);

            var expected = new List<Move>() 
            {
                new Move{ From = fromLocation, To = new Tuple<int, int>(2, 3), IsJump = false}, 
                new Move{ From = fromLocation, To = new Tuple<int, int>(0, 0), IsJump = true}
            };
        }

        /// <summary>
        ///A test for GetValidMoves
        ///</summary>
        [TestMethod()]
        public void GetValidMovesTestXRegular()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();

            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[4][1] = "X";
            Game target = new Game(whitePlayer, blackPlayer, gameBoard);
            
            //Switch the game to black player
            SwitchGamePlayer(target);

            List<Move> actual = target.GetValidMovesRemaining();
            var fromLocation = new Tuple<int,int>(4, 1);

            var expected = new List<Move>() 
            {
                new Move{ From = fromLocation, To = new Tuple<int, int>(5, 0), IsJump = false}, 
                new Move{ From = fromLocation, To = new Tuple<int, int>(5, 2), IsJump = false}
            };
        }

        /// <summary>
        ///A test for GetValidMoves
        ///</summary>
        [TestMethod()]
        public void GetValidMovesTestXJump()
        {
            IPlayer whitePlayer = new ComputerPlayer();
            IPlayer blackPlayer = new ComputerPlayer();

            Board gameBoard = TestHelper.CreateEmptyBoard();
            gameBoard[2][1] = "X";
            gameBoard[3][2] = "O";

            Game target = new Game(whitePlayer, blackPlayer, gameBoard);

            //Switch the game to black player
            SwitchGamePlayer(target);

            List<Move> actual = target.GetValidMovesRemaining();
            var fromLocation = new Tuple<int, int>(2, 1);

            var expected = new List<Move>() 
            {
                new Move{ From = fromLocation, To = new Tuple<int, int>(3, 0), IsJump = false}, 
                new Move{ From = fromLocation, To = new Tuple<int, int>(4, 3), IsJump = true}
            };
        }

        private void SwitchGamePlayer(Game game)
        {
            game.DoMove(new Move() { From = new Tuple<int, int>(0, 0), To = new Tuple<int, int>(0, 0), IsJump = false });
        }
    }
}
