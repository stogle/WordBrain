using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WordBrain
{
    public class Sequence
    {
        private readonly Puzzle _puzzle;
        private readonly WordTree _wordTree;
        private readonly Sequence? _parent;
        private readonly int _i;
        private readonly int _j;
        private string? _toString;

        public Sequence(Puzzle puzzle, WordTree wordTree)
        {
            _puzzle = puzzle;
            _wordTree = wordTree;
            _parent = null;
            _i = -1;
            _j = -1;
            _toString = string.Empty;
            Length = 0;
        }

        private Sequence(Puzzle puzzle, WordTree wordTree, Sequence parent, int i, int j)
        {
            _puzzle = puzzle;
            _wordTree = wordTree;
            _parent = parent;
            _i = i;
            _j = j;
            Length = parent.Length + 1;
        }

        public int Length { get; }

        public IEnumerable<(int i, int j)> GetSquares()
        {
            if (_parent != null)
            {
                foreach ((int, int) square in _parent.GetSquares())
                {
                    yield return square;
                }

                yield return (_i, _j);
            }
        }

        public IEnumerable<Sequence> Extend()
        {
            WordTree? wordTree;
            if (_parent == null)
            {
                for (int i = 0; i < _puzzle.Grid.Height; i++)
                {
                    for (int j = 0; j < _puzzle.Grid.Width; j++)
                    {
                        if (!IsVisited(i, j) && _wordTree.TryLetter(_puzzle.Grid[i, j]!.Value, out wordTree))
                        {
                            yield return new Sequence(_puzzle, wordTree, this, i, j);
                        }
                    }
                }

                yield break;
            }

            if (Length < _puzzle.Solution.NextLength)
            {
                bool isNotFirstColumn = _j - 1 >= 0;
                bool isNotLastColumn = _j + 1 < _puzzle.Grid.Width;
                if (_i - 1 >= 0)
                {
                    if (isNotFirstColumn && !IsVisited(_i - 1, _j - 1) && _wordTree.TryLetter(_puzzle.Grid[_i - 1, _j - 1]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i - 1, _j - 1); // NW
                    }
                    if (!IsVisited(_i - 1, _j) && _wordTree.TryLetter(_puzzle.Grid[_i - 1, _j]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i - 1, _j); // N
                    }
                    if (isNotLastColumn && !IsVisited(_i - 1, _j + 1) && _wordTree.TryLetter(_puzzle.Grid[_i - 1, _j + 1]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i - 1, _j + 1); // NE
                    }
                }
                if (isNotFirstColumn && !IsVisited(_i, _j - 1) && _wordTree.TryLetter(_puzzle.Grid[_i, _j - 1]!.Value, out wordTree))
                {
                    yield return new Sequence(_puzzle, wordTree, this, _i, _j - 1); // W
                }
                if (isNotLastColumn && !IsVisited(_i, _j + 1) && _wordTree.TryLetter(_puzzle.Grid[_i, _j + 1]!.Value, out wordTree))
                {
                    yield return new Sequence(_puzzle, wordTree, this, _i, _j + 1); // E
                }
                if (_i + 1 < _puzzle.Grid.Height)
                {
                    if (isNotFirstColumn && !IsVisited(_i + 1, _j - 1) && _wordTree.TryLetter(_puzzle.Grid[_i + 1, _j - 1]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i + 1, _j - 1); // SW
                    }
                    if (!IsVisited(_i + 1, _j) && _wordTree.TryLetter(_puzzle.Grid[_i + 1, _j]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i + 1, _j); // S
                    }
                    if (isNotLastColumn && !IsVisited(_i + 1, _j + 1) && _wordTree.TryLetter(_puzzle.Grid[_i + 1, _j + 1]!.Value, out wordTree))
                    {
                        yield return new Sequence(_puzzle, wordTree, this, _i + 1, _j + 1); // SE
                    }
                }
            }
        }

        private bool IsVisited(int i, int j)
        {
            Sequence? sequence = this;
            while (sequence._parent != null)
            {
                if (i == sequence._i && j == sequence._j)
                {
                    return true;
                }
                sequence = sequence._parent;
            }
            return _puzzle.Grid[i, j] == null;
        }

        public bool TryPlay([NotNullWhen(true)]out Puzzle? puzzle)
        {
            if (_wordTree.IsWord)
            {
                if (_puzzle.Solution.TryPlay(this, out Solution? solution))
                {
                    Grid grid = _puzzle.Grid.Play(this);
                    puzzle = new Puzzle(grid, solution);
                    return true;
                }
            }

            puzzle = null;
            return false;
        }

        public override string ToString()
        {
            if (_toString == null)
            {
                Sequence? sequence = this;
                char[] buffer = new char[Length];
                for (int i = Length - 1; i >= 0; i--)
                {
                    char? c = _puzzle.Grid[sequence!._i, sequence._j];
                    buffer[i] = c!.Value;
                    sequence = sequence._parent;
                }
                _toString = new string(buffer);
            }

            return _toString;
        }
    }
}
