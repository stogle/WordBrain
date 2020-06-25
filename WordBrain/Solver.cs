using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
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

        public IEnumerable<Solution> Solve(Puzzle puzzle, IProgress<Solution>? progress = null) => SolveAsync(puzzle, progress).ToEnumerable();

        public async IAsyncEnumerable<Solution> SolveAsync(Puzzle puzzle, IProgress<Solution>? progress = null)
        {
            if (puzzle == null)
            {
                throw new ArgumentNullException(nameof(puzzle));
            }

            // Solve each square of the top-level puzzle in parallel
            var channel = Channel.CreateUnbounded<Solution>();
            var sequence = new Sequence(puzzle, _wordTree);
            _ = Task.WhenAll(sequence.Extend()
                .Select(sequence => Task.Run(() =>
                {
                    foreach (Solution solution in Extend(sequence, progress))
                    {
                        channel.Writer.TryWrite(solution);
                    }
                })))
                .ContinueWith(_ => channel.Writer.Complete(), TaskScheduler.Default);

            // Only yield solutions with distinct word sets
            int solutionLength = puzzle.Solution.ToString().Length;
            var distinctSolutions = new HashSet<HashSet<string>>(HashSet<string>.CreateSetComparer());
            await foreach (var solution in channel.Reader.ReadAllAsync())
            {
                var wordSet = new HashSet<string>(solution.Words);
                if (distinctSolutions.Add(wordSet))
                {
                    yield return solution;
                }
            }
        }

        private IEnumerable<Solution> Extend(Sequence sequence, IProgress<Solution>? progress = null)
        {
            if (sequence.TryPlay(out Puzzle? puzzle))
            {
                if (puzzle.Solution.NextLength == null)
                {
                    yield return puzzle.Solution;
                    yield break;
                }

                if (_iteration++ % 5000L == 0L)
                {
                    progress?.Report(puzzle.Solution);
                }

                // Solve remaining puzzle
                foreach (Solution solution in Extend(new Sequence(puzzle, _wordTree), progress))
                {
                    yield return solution;
                }
            }

            // Visit neighbors
            foreach (var nextSequence in sequence.Extend())
            {
                foreach (Solution solution in Extend(nextSequence, progress))
                {
                    yield return solution;
                }
            }
        }
    }
}
