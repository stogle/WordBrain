using System;
using System.Collections.Generic;
using System.IO;

namespace WordBrain
{
    public class Program
    {
        public static void Main(string[] args) => new Program(() => DateTime.Now, File.ReadAllLines, Console.Out, args).Start();

        private readonly Func<DateTime> _now;
        private readonly Func<string, string[]> _fileReader;
        private readonly TextWriter _stdout;
        private readonly string _path;
        private readonly Puzzle _puzzle;
        private readonly Progress<Solution> _progress;
        private int _reportLength;

        public Program(Func<DateTime> now, Func<string, string[]> fileReader, TextWriter stdout, string[] args)
        {
            _now = now ?? throw new ArgumentNullException(nameof(now));
            _fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            _stdout = stdout ?? throw new ArgumentNullException(nameof(stdout));

            var arguments = new Arguments(args, AppDomain.CurrentDomain.FriendlyName);
            if (!arguments.TryParse(out string? path, out char?[][]? letters, out int[]? lengths))
            {
                Environment.ExitCode = -1;
                _stdout.WriteLine(arguments.Usage);
                throw new ArgumentException(Strings.Program_ExpectedValidArgs, nameof(args));
            }
            _path = path;
            _puzzle = new Puzzle(new Grid(letters), new Solution(lengths));
            _progress = new Progress<Solution>(Report);
        }

        public void Start()
        {
            // Read word list
            var start = _now();
            var words = _fileReader(_path);
            var wordTree = new WordTree(words);
            _stdout.WriteLine(Strings.Program_ReadWordsFormat, words.Length, _now() - start);
            _stdout.WriteLine();

            // Solve puzzle
            start = _now();
            _stdout.WriteLine(_puzzle);
            var solutions = Solve(wordTree);
            _stdout.WriteLine();
            _stdout.WriteLine(Strings.Program_FoundSolutionsFormat, solutions.Count, _now() - start);
        }

        private IList<Solution> Solve(WordTree wordTree)
        {
            var solver = new Solver(wordTree);
            var solutions = new List<Solution>();
            foreach (var solution in solver.Solve(_puzzle, _progress))
            {
                solutions.Add(solution);
                ClearReport();
                _stdout.WriteLine(solution);
            }
            ClearReport();
            return solutions;
        }

        private void Report(Solution value)
        {
            if (value == null)
            {
                return;
            }

            string report = value.ToString();
            _reportLength = report.Length;
            Console.Write($"{report}\r");
        }

        private void ClearReport()
        {
            Console.Write($"{new string(' ', _reportLength)}\r");
        }
    }
}
