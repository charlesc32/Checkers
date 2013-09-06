using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Checkers
{
    /// <summary>
    /// Represents a move with the From and To coordinates. Also reports if the move is a jump.
    /// </summary>
    class Move : IEquatable<Move>
    {
        public Tuple<int, int> From { get; set; }
        public Tuple<int, int> To { get; set; }
        public bool IsJump { get; set; }

        public bool Equals(Move other)
        {
            return (other.From.Item1 == From.Item1 && other.To.Item1 == To.Item1 && other.From.Item2 == To.Item2 && other.IsJump == IsJump);
        }
    }
}
