using System.Collections.Generic;
using System.Linq;

namespace Checkers
{
    /// <summary>
    /// This class is a IPlayer that makes moves automatically.
    /// </summary>
    class ComputerPlayer : IPlayer
    {
        public Move GetMove(List<Move> validMoves, Board gameBoard)
        {
            //Do a jump if one is available
            var validJump = validMoves.Select(a => a).Where(b => b.IsJump && gameBoard[b.From.Item1][b.From.Item2] == PieceSymbol).FirstOrDefault();
            if (validJump != null) return validJump;

            var move = validMoves.Select(a => a).Where(b => gameBoard[b.From.Item1][b.From.Item2] == PieceSymbol).FirstOrDefault();
            if (move != null) return move;

            return null;
        }

        public string PieceSymbol { get; set; }
    }
}
