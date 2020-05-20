using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordBrain
{
    public class Move
    {
        private readonly Puzzle _puzzle;
        private readonly Stack<(int i, int j)> _squares = new Stack<(int i, int j)>();
        private readonly StringBuilder _word = new StringBuilder();

        public Move(Puzzle puzzle)
        {
            _puzzle = puzzle ?? throw new ArgumentNullException(nameof(puzzle));
        }

        public void Push(int i, int j)
        {
            if (i < 0 || i >= _puzzle.Grid.Height)
            {
                throw new ArgumentException(string.Format(Strings.Culture, Strings.Move_ExpectedValueInRangeExceptionMessageFormat, 0, _puzzle.Grid.Height - 1), nameof(i));
            }
            if (j < 0 || j >= _puzzle.Grid.Width)
            {
                throw new ArgumentException(string.Format(Strings.Culture, Strings.Move_ExpectedValueInRangeExceptionMessageFormat, 0, _puzzle.Grid.Width - 1), nameof(j));
            }
            if (Count != 0 && (Math.Abs(i - _squares.Peek().i) > 1 || Math.Abs(j - _squares.Peek().j) > 1))
            {
                throw new InvalidOperationException(Strings.Move_InvalidSquareForPushExceptionMessage);
            }

            _squares.Push((i, j));
            _word.Append(_puzzle.Grid[i, j]);
        }

        public void Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException(Strings.Move_InvalidCountForPopExceptionMessage);
            }

            _squares.Pop();
            _word.Remove(_word.Length - 1, 1);
        }

        public Puzzle Play()
        {
            int index = _puzzle.Lengths.IndexOf(_word.Length);
            if (index == -1)
            {
                throw new InvalidOperationException(Strings.Move_InvalidCountForPlayExceptionMessage);
            }

            Grid grid = _puzzle.Grid.Play(_squares);
            int[] lengths = _puzzle.Lengths.Take(index).Concat(_puzzle.Lengths.Skip(index + 1)).ToArray();
            return new Puzzle(grid, lengths);
        }

        public int Count => _word.Length;

        public override string ToString() => _word.ToString();
    }
}
