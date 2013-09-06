using System.Collections.Generic;

namespace Checkers
{
    /// <summary>
    /// Interface that defines a Player. A Player must be able to tell which move it wants to make and must know which pieces it is using.
    /// </summary>
    interface IPlayer
    {
        string PieceSymbol { get; set; }
        Move GetMove(List<Move> validMoves, Board gameBoard);
    }
}
