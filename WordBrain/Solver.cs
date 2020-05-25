using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public IEnumerable<Solution> Solve(Puzzle puzzle)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException(nameof(puzzle));
            }

            // Solve each square of the top-level puzzle in parallel
            using var solutions = new BlockingCollection<Solution>();
            Task.Run(() =>
            {
                var sequence = new Sequence(puzzle, _wordTree);
                Parallel.ForEach(sequence.Extend(), sequence =>
                {
                    foreach (Solution solution in Extend(sequence))
                    {
                        solutions.Add(solution);
                    }
                });
                solutions.CompleteAdding();
            });

            // Only yield solutions with distinct word sets
            int solutionLength = puzzle.Solution.ToString().Length;
            var distinctSolutions = new HashSet<HashSet<string>>(HashSet<string>.CreateSetComparer());
            foreach (var solution in solutions.GetConsumingEnumerable())
            {
                var wordSet = new HashSet<string>(solution.Words);
                if (distinctSolutions.Add(wordSet))
                {
                    Console.Write($"{new string(' ', solutionLength)}\r");

                    yield return solution;
                }
            }

            Console.Write($"{new string(' ', solutionLength)}\r");
        }

        private IEnumerable<Solution> Extend(Sequence sequence)
        {
            Puzzle puzzle;
            if (sequence.TryPlay(out puzzle!))
            {
                if (puzzle.Solution.MaxLength == null)
                {
                    yield return puzzle.Solution;
                    yield break;
                }

                if (_iteration++ % 5000L == 0L)
                {
                    Console.Write($"{puzzle.Solution}\r");
                }

                // Solve remaining puzzle
                foreach (Solution solution in Extend(new Sequence(puzzle, _wordTree)))
                {
                    yield return solution;
                }
            }

            // Visit neighbors
            foreach (var nextSequence in sequence.Extend())
            {
                foreach (Solution solution in Extend(nextSequence))
                {
                    yield return solution;
                }
            }
        }
    }
}
