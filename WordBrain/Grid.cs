using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Grid
    {
        private readonly char?[][] _letters;

        internal Grid(char?[][] letters)
        {
            Height = letters.Length;
            Width = Height == 0 ? 0 : letters[0].Length;
            _letters = letters;
        }

        public int Height { get; }

        public int Width { get; }

        public char? this[int x, int y] => _letters[x][y];

        internal bool TryPlay(IEnumerable<(int i, int j)> sequence, out Grid? grid)
        {
            // Set all used letter to null
            char?[][] letters = _letters.Select(row => row.ToArray()).ToArray();
            foreach ((int i, int j) in sequence)
            {
                if (i < 0 || i >= Height || j < 0 || j >= Width || letters[i][j] == null)
                {
                    grid = null;
                    return false;
                }

                letters[i][j] = null;
            }

            // Fill null letters with the letters above
            for (int j = 0; j < Width; j++)
            {
                for (int i = Height - 1; i > 0; i--)
                {
                    if (letters[i][j] == null)
                    {
                        int k = i - 1;
                        while (k >= 0 && letters[k][j] == null)
                        {
                            k--;
                        }
                        if (k >= 0)
                        {
                            letters[i][j] = letters[k][j];
                            letters[k][j] = null;
                        }
                    }
                }
            }

            grid = new Grid(letters);
            return true;
        }

        public override string ToString() => string.Join(Environment.NewLine, _letters.Select(row => string.Join(' ', row.Select(c => c ?? '.'))));
    }
}
