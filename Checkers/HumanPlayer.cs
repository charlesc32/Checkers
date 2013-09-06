using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    /// <summary>
    /// The HumanPlayer is a IPlayer that prompts for moves and validates moves chosen by the user.
    /// </summary>
    class HumanPlayer : IPlayer
    {

        public string PieceSymbol { get; set; }

        public string PromptForColor()
        {
            Console.Write("Which color do you want to be? Black or White? [B][W]: ");
            string colorChoice = Console.ReadLine();

            if (colorChoice.ToUpper() != "B" && colorChoice.ToUpper() != "W")
            {
                Console.WriteLine("Please pick either B or W for a color choice.");
                Console.WriteLine();
                colorChoice = PromptForColor();
            }

            PieceSymbol = (colorChoice.ToUpper() == "W") ? "O" : "X";

            return colorChoice;
        }

        public Move GetMove(List<Move> validMoves, Board gameBoard)
        {
            int row = GetLocation("row", "from");
            int col = GetLocation("col", "from");

            var from = new Tuple<int, int>(row, col);

            row = GetLocation("row", "to");
            col = GetLocation("col", "to");
            Console.WriteLine();

            var to = new Tuple<int, int>(row, col);

            var move = new Move() {From = from, To = to};
            
            if (ValidateMove(move, validMoves, gameBoard)) return move;

            return GetMove(validMoves, gameBoard);
        }

        private int GetLocation(string type, string direction)
        {
            Console.Write("Enter " + type + " of piece you wish to move " + direction + ": ");
            int pos;
            bool error = false;

            if (int.TryParse(Console.ReadLine(), out pos))
            {
                if (pos > 7 || pos < 0) error = true;
            }
            else
            {
                error = true;
            }

            if (error)
            {
                Console.WriteLine("Please enter a valid " + type + ".");
                Console.WriteLine();
                pos = GetLocation(type, direction);
            }
            
            return pos;
        }

        private bool ValidateMove(Move move, List<Move> validMoves, Board gameBoard)
        {
            int rowFrom = move.From.Item1;
            int colFrom = move.From.Item2;
            int rowTo = move.To.Item1;
            int colTo = move.To.Item2;

            move.IsJump = (Math.Abs(rowFrom - rowTo) > 1);

            if (!move.IsJump)
            {
                var availableJump = validMoves.Select(a => a).Where(b => b.IsJump).FirstOrDefault();
                if (availableJump != null)
                {
                    Console.WriteLine("If there is a jump move available you MUST make the jump! Try again.");
                    Console.WriteLine();
                    return false;
                }
            }

            var validMove = validMoves.Select(a => a).Where(b => (b.From.Item1 == rowFrom) && (b.From.Item2 == colFrom) && (b.To.Item1 == rowTo) && (b.To.Item2 == colTo) && (gameBoard[rowFrom][colFrom] == PieceSymbol)).FirstOrDefault();
            if (validMove != null) return true;

            string errorMessage = string.Empty;
            
            if (gameBoard[rowTo][colTo] != ".") errorMessage = "Sorry. That space you are moving to is already occupied.";
            if ((PieceSymbol == "X" && rowFrom >= rowTo) || (PieceSymbol == "O" && rowFrom <= rowTo)) errorMessage = "You can only move forward!";
            if (gameBoard[rowFrom][colFrom] != PieceSymbol) errorMessage = "Please pick a valid piece to move.";

            if (errorMessage == string.Empty) errorMessage = "Invalid move. Try again.";

            Console.WriteLine(errorMessage);
            Console.WriteLine();

            Console.WriteLine(gameBoard.ToString());

            return false;
        }
    }
}
