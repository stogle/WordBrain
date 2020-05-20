using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WordBrain
{
    public class Puzzle
    {
        public Puzzle(Grid grid, int[] lengths)
        {
            if (grid == null)
            {
                throw new ArgumentNullException(nameof(grid));
            }
            if (lengths == null)
            {
                throw new ArgumentNullException(nameof(lengths));
            }
            if (lengths.Any(length => length <= 0))
            {
                throw new ArgumentException(Strings.Grid_ExpectedPositiveLengthsExceptionMessage, nameof(lengths));
            }
            if (lengths.Sum() != grid.RemainingLetters)
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedLengthsSumEqualToLettersSizeExceptionMessage, nameof(lengths));
            }

            Grid = grid;
            Lengths = Array.AsReadOnly(lengths);
        }

        public Grid Grid { get; }

        public ReadOnlyCollection<int> Lengths { get; }

        public override string ToString() => $"{Grid}{Environment.NewLine}{string.Join(' ', Lengths.Select(n => new string('_', n)))}{Environment.NewLine}";
    }
}
