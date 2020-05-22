using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Solver
    {
        private readonly WordTree _wordTree;
        private long _iteration;

        public Solver(WordTree wordTree)
        {
            _wordTree = wordTree ?? throw new ArgumentNullException(nameof(wordTree));
        }

        public IEnumerable<string> Solve(Puzzle puzzle)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException(nameof(puzzle));
            }

            return SolveTop(puzzle)
                .Select(solution => solution.ToString())
                .Distinct();
        }

        private IEnumerable<Solution> SolveTop(Puzzle puzzle)
        {
            int solutionLength = puzzle.Solution.ToString().Length;
            foreach (var solution in SolveInternal(puzzle))
            {
                Console.Write($"\r{string.Empty.PadRight(solutionLength)}\r");

                yield return solution;
            }

            Console.Write($"\r{string.Empty.PadRight(solutionLength)}\r");
        }

        private IEnumerable<Solution> SolveInternal(Puzzle puzzle)
        {
            if (puzzle.Solution.IsComplete)
            {
                yield return puzzle.Solution;
            }

            bool[][] visited = Enumerable.Range(0, puzzle.Grid.Height).Select(i => Enumerable.Range(0, puzzle.Grid.Width).Select(j => puzzle.Grid[i, j] == null).ToArray()).ToArray();
            for (int i = 0; i < puzzle.Grid.Height; i++)
            {
                for (int j = 0; j < puzzle.Grid.Width; j++)
                {
                    if (!visited[i][j])
                    {
                        foreach (Solution solution in Visit(puzzle, i, j, visited, new Stack<(int i, int j)>(), _wordTree))
                        {
                            yield return solution;
                        }
                    }
                }
            }
        }

        private IEnumerable<Solution> Visit(Puzzle puzzle, int i, int j, bool[][] visited, Stack<(int i, int j)> currentMove, WordTree currentWordTree)
        {
            char letter = puzzle.Grid[i, j]!.Value; // We only visit squares with non-null letters
            if (currentWordTree.TryLetter(letter, ref currentWordTree))
            {
                visited[i][j] = true;
                currentMove.Push((i, j));

                if (currentWordTree.IsWord)
                {
                    if (puzzle.TryPlay(currentMove.Reverse(), out Puzzle? currentPuzzle))
                    {
                        if (_iteration++ % 1000L == 0L)
                        {
                            Console.Write($"\r{currentPuzzle!.Solution}");
                        }

                        foreach (Solution solution in SolveInternal(currentPuzzle!))
                        {
                            yield return solution;
                        }
                    }
                }

                // Visit neighbors
                for (int x = i - 1; x <= i + 1; x++)
                {
                    if (x >= 0 && x < puzzle.Grid.Height)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (y >= 0 && y < puzzle.Grid.Width)
                            {
                                if (!visited[x][y])
                                {
                                    foreach (Solution solution in Visit(puzzle, x, y, visited, currentMove, currentWordTree))
                                    {
                                        yield return solution;
                                    }
                                }
                            }
                        }
                    }
                }

                currentMove.Pop();
                visited[i][j] = false;
            }
        }
    }
}
