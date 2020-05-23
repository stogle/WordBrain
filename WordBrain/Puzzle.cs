using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Puzzle
    {
        public Puzzle(char?[][] letters, int[] lengths)
        {
            if (letters == null)
            {
                throw new ArgumentNullException(nameof(letters));
            }
            if (letters.Skip(1).Any(row => row.Length != letters[0].Length))
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedRectangularLettersExceptionMessage, nameof(letters));
            }
            if (lengths == null)
            {
                throw new ArgumentNullException(nameof(lengths));
            }
            if (lengths.Any(length => length <= 0))
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedPositiveLengthsExceptionMessage, nameof(lengths));
            }
            if (lengths.Sum() != letters.SelectMany(row => row).Count(c => c != null))
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedRemainingLettersEqualExceptionMessage, nameof(lengths));
            }

            Grid = new Grid(letters);
            Solution = new Solution(lengths);
        }

        internal Puzzle(Grid grid, Solution solution)
        {
            Grid = grid;
            Solution = solution;
        }

        public Grid Grid { get; }

        public Solution Solution { get; }

        public bool TryPlay(IEnumerable<(int i, int j)> sequence, out Puzzle? puzzle)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (Grid.TryPlay(sequence, out Grid? grid))
            {
                string word = string.Concat(sequence.Select(square => Grid[square.i, square.j]));
                if (Solution.TryPlay(word, out Solution? solution))
                {
                    puzzle = new Puzzle(grid!, solution!);
                    return true;
                }
            }

            puzzle = null;
            return false;
        }

        public override string ToString() => $"{Grid}{Environment.NewLine}{Solution}{Environment.NewLine}";
    }
}
