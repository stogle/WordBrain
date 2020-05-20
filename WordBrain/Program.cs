using System;
using System.Diagnostics;
using System.IO;

namespace WordBrain
{
    /// <example>
    /// > WordBrain CSW19.txt SLLY HAUE ICTN PAON 4 8 4
    /// 
    /// S L L Y
    /// H A U E
    /// I C T N
    /// P A O N
    /// ____ ________ ____
    /// 
    /// SHIP ACTUALLY NONE
    /// NONE ACTUALLY SHIP
    /// 
    /// Found 2 solutions in 00:00:01.9090980
    /// </example>
    /// <example>
    /// > WordBrain CSW19.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12
    /// 
    /// E Z E A I
    /// U I C R D
    /// L R H A D
    /// A E A E O
    /// V C T E S
    /// _____ ____ ____ ____________
    /// 
    /// VALUE ADOS EIDE CHARACTERIZE
    /// VALUE ADOS IDEE CHARACTERIZE
    /// VALUE DOES AIDE CHARACTERIZE
    /// VALUE DOES IDEA CHARACTERIZE
    /// VALUE DOSE AIDE CHARACTERIZE
    /// VALUE DOSE IDEA CHARACTERIZE
    /// VALUE ODEA IDES CHARACTERIZE
    /// VALUE ODES AIDE CHARACTERIZE
    /// VALUE ODES IDEA CHARACTERIZE
    /// VALUE SODA EIDE CHARACTERIZE
    /// VALUE SODA IDEE CHARACTERIZE
    /// 
    /// Found 11 solutions in 00:36:27.2480340
    /// </example>
    public static class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new Arguments(args, AppDomain.CurrentDomain.FriendlyName);
            if (!arguments.IsValid)
            {
                Console.WriteLine(arguments.Usage);
                Environment.Exit(-1);
            }

            var puzzle = arguments.Puzzle!;
            Console.WriteLine(puzzle);
            var wordTree = new WordTree(File.ReadAllLines(arguments.Path));

            var stopWatch = Stopwatch.StartNew();
            var solver = new Solver(wordTree);
            var solutions = solver.Solve(puzzle);
            int count = 0;
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
                count++;
            }
            Console.WriteLine();
            Console.WriteLine($"Found {count} solutions in {stopWatch.Elapsed}");
        }
    }
}
