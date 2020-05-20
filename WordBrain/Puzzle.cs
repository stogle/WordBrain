using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Puzzle
    {
        public Puzzle(Grid grid, Solution solution)
        {
            Grid = grid ?? throw new ArgumentNullException(nameof(grid));
            Solution = solution ?? throw new ArgumentNullException(nameof(solution));

            if (solution.RemainingLetters != grid.RemainingLetters)
            {
                throw new ArgumentException(Strings.Puzzle_ExpectedRemainingLettersEqualExceptionMessage, nameof(solution));
            }
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
