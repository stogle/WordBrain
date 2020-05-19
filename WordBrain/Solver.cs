using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Solver
    {
        private readonly WordTree _wordTree;

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
                .Select(solution => string.Join(' ', solution.Select(move => move.ToString())))
                .Distinct();
        }

        private IEnumerable<List<Move>> SolveTop(Puzzle puzzle)
        {
            foreach (var solution in SolveInternal(puzzle))
            {
                Console.Write(string.Concat(Enumerable.Repeat("\b \b", puzzle.Width * puzzle.Height + solution.Count)));
                yield return solution;
            }
        }

        private IEnumerable<List<Move>> SolveInternal(Puzzle puzzle)
        {
            bool[][] visited = Enumerable.Range(0, puzzle.Height).Select(i => Enumerable.Range(0, puzzle.Width).Select(j => puzzle[i, j] == null).ToArray()).ToArray();
            if (puzzle.Lengths.Any())
            {
                for (int i = 0; i < puzzle.Height; i++)
                {
                    for (int j = 0; j < puzzle.Width; j++)
                    {
                        if (!visited[i][j])
                        {
                            foreach (List<Move> solution in Visit(puzzle, i, j, visited, new Move(puzzle), _wordTree))
                            {
                                yield return solution;
                            }
                        }
                    }
                }

                yield break;
            }

            yield return new List<Move>();
        }

        private IEnumerable<List<Move>> Visit(Puzzle puzzle, int i, int j, bool[][] visited, Move currentMove, WordTree currentWordTree)
        {
            char letter = puzzle[i, j]!.Value; // We only visit squares with non-null letters
            if (currentWordTree.TryLetter(letter, ref currentWordTree))
            {
                visited[i][j] = true;
                currentMove.Push(i, j);

                if (currentWordTree.IsWord)
                {
                    int index = puzzle.Lengths.IndexOf(currentMove.Count);
                    if (index != -1)
                    {
                        Console.Write($"{currentMove} ");

                        foreach (List<Move> solution in SolveInternal(currentMove.Play()))
                        {
                            solution.Insert(index, currentMove);
                            yield return solution;
                        }

                        Console.Write(string.Concat(Enumerable.Repeat("\b \b", currentMove.Count + 1)));
                    }
                }

                // Visit neighbours
                for (int x = i - 1; x <= i + 1; x++)
                {
                    if (x >= 0 && x < puzzle.Height)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            if (y >= 0 && y < puzzle.Width)
                            {
                                if (!visited[x][y])
                                {
                                    foreach (List<Move> solution in Visit(puzzle, x, y, visited, currentMove, currentWordTree))
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
