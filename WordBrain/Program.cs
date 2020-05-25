using System;
using System.Diagnostics;
using System.IO;

namespace WordBrain
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Process arguments
            var arguments = new Arguments(args, AppDomain.CurrentDomain.FriendlyName);
            if (!arguments.IsValid)
            {
                Console.WriteLine(arguments.Usage);
                Environment.Exit(-1);
            }
            string path = arguments.Path!;
            Puzzle puzzle = arguments.Puzzle!;

            // Read word list
            var stopWatch = Stopwatch.StartNew();
            var words = File.ReadAllLines(path);
            var wordTree = new WordTree(words);
            Console.WriteLine(Strings.Program_ReadWordsFormat, words.Length, stopWatch.Elapsed);
            Console.WriteLine();

            // Solve puzzle
            Console.WriteLine(puzzle);
            var solver = new Solver(wordTree);
            int count = 0;
            stopWatch.Restart();
            var solutions = solver.Solve(puzzle);
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
                count++;
            }
            Console.WriteLine();
            Console.WriteLine(Strings.Program_FoundSolutionsFormat, count, stopWatch.Elapsed);
        }
    }
}
