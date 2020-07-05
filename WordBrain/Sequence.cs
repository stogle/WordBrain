using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WordBrain
{
    public class Sequence
    {
        private readonly Puzzle _puzzle;
        private readonly WordTree _wordTree;
        private readonly Sequence? _parent;
        private readonly int _row;
        private readonly int _col;
        private string? _toString;

        public Sequence(Puzzle puzzle, WordTree wordTree)
        {
            _puzzle = puzzle;
            _wordTree = wordTree;
            _parent = null;
            _row = -1;
            _col = -1;
            _toString = string.Empty;
            Length = 0;
        }

        private Sequence(Sequence parent, WordTree wordTree, int row, int col)
        {
            _puzzle = parent._puzzle;
            _wordTree = wordTree;
            _parent = parent;
            _row = row;
            _col = col;
            Length = parent.Length + 1;
        }

        public int Length { get; }

        public IEnumerable<(int row, int col)> GetSquares()
        {
            if (_parent != null)
            {
                foreach ((int, int) square in _parent.GetSquares())
                {
                    yield return square;
                }

                yield return (_row, _col);
            }
        }

        public IEnumerable<Sequence> Extend()
        {
            char? letter;
            WordTree? childTree;
            if (_parent == null)
            {
                for (int row = 0; row < _puzzle.Grid.Height; row++)
                {
                    for (int col = 0; col < _puzzle.Grid.Width; col++)
                    {
                        if (TryVisit(row, col, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                        {
                            yield return new Sequence(this, childTree, row, col);
                        }
                    }
                }

                yield break;
            }

            if (Length < _puzzle.Solution.NextLength)
            {
                bool isNotFirstColumn = _col - 1 >= 0;
                bool isNotLastColumn = _col + 1 < _puzzle.Grid.Width;
                if (_row - 1 >= 0)
                {
                    if (isNotFirstColumn && TryVisit(_row - 1, _col - 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row - 1, _col - 1); // NW
                    }
                    if (TryVisit(_row - 1, _col, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row - 1, _col); // N
                    }
                    if (isNotLastColumn && TryVisit(_row - 1, _col + 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row - 1, _col + 1); // NE
                    }
                }
                if (isNotFirstColumn && TryVisit(_row, _col - 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                {
                    yield return new Sequence(this, childTree, _row, _col - 1); // W
                }
                if (isNotLastColumn && TryVisit(_row, _col + 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                {
                    yield return new Sequence(this, childTree, _row, _col + 1); // E
                }
                if (_row + 1 < _puzzle.Grid.Height)
                {
                    if (isNotFirstColumn && TryVisit(_row + 1, _col - 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row + 1, _col - 1); // SW
                    }
                    if (TryVisit(_row + 1, _col, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row + 1, _col); // S
                    }
                    if (isNotLastColumn && TryVisit(_row + 1, _col + 1, out letter) && _wordTree.TryLetter(letter.Value, out childTree))
                    {
                        yield return new Sequence(this, childTree, _row + 1, _col + 1); // SE
                    }
                }
            }
        }

        private bool TryVisit(int row, int col, [NotNullWhen(true)]out char? letter)
        {
            Sequence? sequence = this;
            while (sequence._parent != null)
            {
                if (row == sequence._row && col == sequence._col)
                {
                    letter = null;
                    return false;
                }
                sequence = sequence._parent;
            }
            letter = _puzzle.Grid[row, col];
            return letter != null;
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
                    char? c = _puzzle.Grid[sequence!._row, sequence._col];
                    buffer[i] = c!.Value;
                    sequence = sequence._parent;
                }
                _toString = new string(buffer);
            }

            return _toString;
        }
    }
}
