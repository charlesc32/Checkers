using System;
using System.Linq;
using System.Collections.Generic;

namespace Checkers
{
    /// <summary>
    /// The Game class represents a game of checkers.
    /// A game of checkers consists of a game board, a player that uses the white pieces and a player that uses the black pieces.
    /// The game keeps track of whose turn it is, makes moves on the board and reports on valid moves.
    /// </summary>
    class Game
    {
        public Board GameBoard { get; set; }

        public IPlayer WhitePlayer { get; set; }
        public IPlayer BlackPlayer { get; set; }

        private IPlayer CurrentPlayer;

        List<Move> validMoves;

        public string PieceSymbol 
        { 
            get
            {
                return (CurrentPlayer.Equals(WhitePlayer)) ? "O" : "X";
            }
        }

        public Game(IPlayer whitePlayer, IPlayer blackPlayer, Board gameBoard)
        {
            if (whitePlayer == null || blackPlayer == null || gameBoard == null) throw new ArgumentException("Null arguments passed to Game constructor.");

            Init(whitePlayer, blackPlayer, gameBoard);
        }

        private void Init(IPlayer whitePlayer, IPlayer blackPlayer, Board gameBoard)
        {
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;

            GameBoard = gameBoard;

            CurrentPlayer = whitePlayer;
        }

        public Move GetNextMove()
        {
            var move = CurrentPlayer.GetMove(validMoves, GameBoard);
            return move;
        }

        public void DoMove(Move move)
        {
            UpdateBoard(move);

            if (CurrentPlayer.GetType() == typeof(ComputerPlayer))
            {
                Console.WriteLine("Computer moved from [" + move.From.Item1 + "," + move.From.Item2 + "] to [" + move.To.Item1 + "," + move.To.Item2 + "]");
                Console.WriteLine();
            }

            
            CurrentPlayer = (CurrentPlayer.Equals(WhitePlayer)) ? BlackPlayer : WhitePlayer;
        }

        private void UpdateBoard(Move move)
        {
            int rowFrom = move.From.Item1;
            int colFrom = move.From.Item2;
            int rowTo = move.To.Item1;
            int colTo = move.To.Item2;

            GameBoard[rowFrom][colFrom] = ".";
            GameBoard[rowTo][colTo] = PieceSymbol;
            if (move.IsJump)
            {
                var jumpedLocation = GetJumpedPieceCoordinates(rowFrom, colFrom, rowTo, colTo);
                GameBoard[jumpedLocation.Item1][jumpedLocation.Item2] = ".";
            }
        }

        private Tuple<int, int> GetJumpedPieceCoordinates(int fromRow, int fromCol, int toRow, int toCol)
        {
            int rowOfJumpedPiece = fromRow + ((fromRow > toRow) ? -1 : 1);
            int colOfJumpedPiece = fromCol + ((fromCol > toCol) ? -1 : 1);

            return new Tuple<int, int>(rowOfJumpedPiece, colOfJumpedPiece);
        }

        internal void PrintBoard()
        {
            Console.WriteLine(GameBoard.ToString());
        }

        public List<Move> GetValidMovesRemaining()
        {
            validMoves = GetValidMoves(CurrentPlayer.PieceSymbol);
            return validMoves;
        }

        private List<Move> GetValidMoves(string pieceType)
        {
            var moves = new List<Move>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (GameBoard[i][j] == pieceType)
                    {
                        //Normal moves
                        int rowMovement = (pieceType == "X") ? 1 : -1;
                        int newRow = i + rowMovement;
                        
                        //Left
                        int newCol = j - 1;
                        Move newMove = CreateMoveIfValid(i, j, newRow, newCol, false);
                        if (newMove != null) moves.Add(newMove);
                        
                        //Right
                        newCol = j + 1;
                        newMove = CreateMoveIfValid(i, j, newRow, newCol, false);
                        if (newMove != null) moves.Add(newMove);

                        //Jumps
                        rowMovement = (pieceType == "X") ? 2 : -2;
                        newRow = i + rowMovement;

                        //Left
                        newCol = j - 2;
                        newMove = CreateMoveIfValid(i, j, newRow, newCol, true);
                        if (newMove != null) moves.Add(newMove);

                        //Right
                        newCol = j + 2;
                        newMove = CreateMoveIfValid(i, j, newRow, newCol, true);
                        if (newMove != null) moves.Add(newMove);
                    }
                }
            }

            return moves;
        }

        private Move CreateMoveIfValid(int fromRow, int fromCol, int toRow, int toCol, bool isJump)
        {
            Move newMove = null;

            if (toCol >= 0 && toCol < 8 && toRow >= 0 && toRow < 8)
            {
                if (GameBoard[toRow][toCol] == ".")
                {
                    if (isJump)
                    {
                        var jumpedSquareLocation = GetJumpedPieceCoordinates(fromRow, fromCol, toRow, toCol);
                        var jumpedSquareValue = GameBoard[jumpedSquareLocation.Item1][jumpedSquareLocation.Item2];
                        if (GameBoard[fromRow][fromCol] == jumpedSquareValue || jumpedSquareValue == ".") return null;
                    }
                    newMove = new Move() { From = new Tuple<int, int>(fromRow, fromCol), To = new Tuple<int, int>(toRow, toCol), IsJump = isJump };
                }
            }

            return newMove;
        }

        internal int GetRemainingPieces(string color)
        {
            int remainingPieces = 0;

            string pieceToLookFor = (color.ToLower() == "white") ? "O" : "X";

            remainingPieces = GameBoard.SelectMany(a => a).ToList().Where(b => b == pieceToLookFor).Count();

            return remainingPieces;
        }
    }
}
