using System;
using System.Diagnostics;
using System.IO;

namespace WordBrain
{
    /// <example>
    /// The command <c>WordBrain nwl2018.txt SLLY HAUE ICTN PAON 4 8 4</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.4762284
    /// 
    /// S L L Y
    /// H A U E
    /// I C T N
    /// P A O N
    /// ____ ________ ____
    /// 
    /// SHIP ACTUALLY NONE
    /// 
    /// Found 1 solutions in 00:00:00.1665678
    /// </code>
    /// </example>
    ///
    /// <example>
    /// The command <c>WordBrain nwl2018.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.4885625
    /// 
    /// E Z E A I
    /// U I C R D
    /// L R H A D
    /// A E A E O
    /// V C T E S
    /// _____ ____ ____ ____________
    /// 
    /// VALUE ADOS CHARACTERIZE EIDE
    /// VALUE DOES CHARACTERIZE AIDE
    /// VALUE DOES CHARACTERIZE IDEA
    /// VALUE DOSE CHARACTERIZE AIDE
    /// VALUE DOSE CHARACTERIZE IDEA
    /// VALUE ODEA CHARACTERIZE IDES
    /// VALUE ODES CHARACTERIZE AIDE
    /// VALUE ODES CHARACTERIZE IDEA
    /// VALUE SODA CHARACTERIZE EIDE
    /// 
    /// Found 9 solutions in 00:01:42.0179057
    /// </code>
    /// </example>
    ///
    /// <example>
    /// The command <c>WordBrain nwl2018.txt G....T AF..AL PL..LI TO..EE TAM.UR ETO.MB 6 4 8 3 3</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.4621304
    /// 
    /// G . . . . T
    /// A F . . A L
    /// P L . . L I
    /// T O . . E E
    /// T A M . U R
    /// E T O . M B
    /// ______ ____ ________ ___ ___
    /// 
    /// GAP TOMATO FELT UMBRELLA TIE
    /// GAP TOMATO UMBRELLA TIE LEFT
    /// FAG ATOP MOTTLE UMBRELLA TIE
    /// 
    /// Found 3 solutions in 00:01:32.9075473
    /// </code>
    /// </example>
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
