using System;
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

        public override string ToString() => $"{Grid}{Environment.NewLine}{Solution}{Environment.NewLine}";
    }
}
