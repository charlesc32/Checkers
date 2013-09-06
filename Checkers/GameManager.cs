using System;
using System.Collections.Generic;
namespace Checkers
{
    /// <summary>
    /// The GameManager runs a Game of checkers.
    /// A game can be between a human player and a computer or between two computer players.
    /// The game continues as long as there are valid moves left or until one player wins.
    /// </summary>
    class GameManager
    {
        private Game game;
        public Game Game { get { return game; } }

        private string winner;
        public string Winner { get {return winner;} }

        private bool isDraw;
        public bool IsDraw { get {return isDraw;} }

        public GameManager()
        {
            IPlayer human = new HumanPlayer();
            IPlayer computer = new ComputerPlayer();
            
            string color = ((HumanPlayer)human).PromptForColor();
            color = color.ToUpper();
            
            IPlayer whitePlayer = (color == "B") ? computer : human;
            IPlayer blackPlayer = (color == "B") ? human : computer;

            Init(whitePlayer, blackPlayer);
        }

        public GameManager(IPlayer whitePlayer, IPlayer blackPlayer, Board board = null)
        {
            Init(whitePlayer, blackPlayer, board);
        }

        private void Init(IPlayer whitePlayer, IPlayer blackPlayer, Board board = null)
        {
            if (whitePlayer == null) whitePlayer = new ComputerPlayer();
            if (blackPlayer == null) blackPlayer = new ComputerPlayer();
            if (board == null) board = new Board();

            whitePlayer.PieceSymbol = "O";
            blackPlayer.PieceSymbol = "X";

            game = new Game(whitePlayer, blackPlayer, board);

            GameLoop();
        }

        private void GameLoop()
        {
            var validMoves = new List<Move>();
            int whiteRemainingPieces = 0;
            int blackRemainingPieces = 0;

            do
            {
                game.PrintBoard();
                validMoves = game.GetValidMovesRemaining();

                var move = game.GetNextMove();
                game.DoMove(move);

                whiteRemainingPieces = CheckForWinState("White");
                if (whiteRemainingPieces == 0) break;
                blackRemainingPieces = CheckForWinState("Black");
                if (blackRemainingPieces == 0) break;
                
                validMoves = game.GetValidMovesRemaining();

            } while (validMoves.Count > 0);

            if (blackRemainingPieces > 0 && whiteRemainingPieces > 0)
            {
                Console.WriteLine("No valid moves left. Draw game! Thanks for playing.");
                isDraw = true;
            }
        }

        private int CheckForWinState(string color)
        {
            int piecesRemaining = game.GetRemainingPieces(color);
            if (piecesRemaining == 0)
            {
                string winningColor = (color.ToLower() == "white") ? "Black" : "White";
                Console.WriteLine(winningColor + " wins!");
                winner = winningColor;
            }

            return piecesRemaining;
        }
    }
}
