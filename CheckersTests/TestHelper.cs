using System.Collections.Generic;
using Checkers;

namespace CheckersTests
{
    static class TestHelper
    {
        public static Board CreateEmptyBoard()
        {
            var board = new Board();
            board.Clear();

            for (int i = 0; i < 8; i++)
            {
                board.Add(new List<string>());
                for (int j = 0; j < 8; j++)
                {
                    board[i].Add(".");
                }
            }

            return board;
        }
    }
}
