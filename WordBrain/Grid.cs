using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Grid
    {
        private readonly IReadOnlyList<IReadOnlyList<char?>> _letters;

        internal Grid(IReadOnlyList<IReadOnlyList<char?>> letters)
        {
            Height = letters.Count;
            Width = Height == 0 ? 0 : letters[0].Count;
            _letters = letters;
        }

        public int Height { get; }

        public int Width { get; }

        public char? this[int row, int col] => _letters[row][col];

        internal Grid Play(Sequence sequence)
        {
            // Set all used letter to null
            char?[][] letters = _letters.Select(row => row.ToArray()).ToArray();
            foreach ((int row, int col) in sequence.GetSquares())
            {
                letters[row][col] = null;
            }

            // Fill null letters with the letters above
            for (int col = 0; col < Width; col++)
            {
                for (int row = Height - 1; row > 0; row--)
                {
                    if (letters[row][col] == null)
                    {
                        int rowAbove = row - 1;
                        while (rowAbove >= 0 && letters[rowAbove][col] == null)
                        {
                            rowAbove--;
                        }
                        if (rowAbove >= 0)
                        {
                            letters[row][col] = letters[rowAbove][col];
                            letters[rowAbove][col] = null;
                        }
                    }
                }
            }

            return new Grid(letters);
        }

        public override string ToString() => string.Join(Environment.NewLine, _letters.Select(row => string.Join(' ', row.Select(c => c ?? '.'))));
    }
}
