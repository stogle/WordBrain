using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WordBrain
{
    public class Puzzle
    {
        private readonly char?[][] _letters;

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
            if (lengths.Sum() != letters.SelectMany(row => row).Count(letter => letter != null))
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedLengthsSumEqualToLettersSizeExceptionMessage, nameof(lengths));
            }

            Height = letters.Length;
            Width = letters[0].Length;
            _letters = letters;
            Lengths = Array.AsReadOnly(lengths);
        }

        public int Height { get; }

        public int Width { get; }

        public char? this[int x, int y] => _letters[x][y];

        public ReadOnlyCollection<int> Lengths { get; }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < Height; i++)
            {
                result.AppendLine(string.Join(' ', Enumerable.Range(0, Width).Select(j => _letters[i][j] ?? '.')));
            }

            result.AppendLine(string.Join(' ', Lengths.Select(n => new string('_', n))));

            return result.ToString();
        }
    }
}
