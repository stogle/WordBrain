using System;
using System.Diagnostics;
using System.IO;

namespace WordBrain
{
    /// <example>
    /// The command <c>WordBrain nwl2018.txt SLLY HAUE ICTN PAON 4 8 4</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.4664177
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
    /// Found 2 solutions in 00:00:00.1702710
    /// </code>
    /// </example>
    ///
    /// <example>
    /// The command <c>WordBrain nwl2018.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.5089258
    /// 
    /// E Z E A I
    /// U I C R D
    /// L R H A D
    /// A E A E O
    /// V C T E S
    /// _____ ____ ____ ____________
    /// 
    /// VALUE ADOS EIDE CHARACTERIZE
    /// VALUE DOES AIDE CHARACTERIZE
    /// VALUE DOES IDEA CHARACTERIZE
    /// VALUE DOSE AIDE CHARACTERIZE
    /// VALUE DOSE IDEA CHARACTERIZE
    /// VALUE ODEA IDES CHARACTERIZE
    /// VALUE ODES AIDE CHARACTERIZE
    /// VALUE ODES IDEA CHARACTERIZE
    /// VALUE SODA EIDE CHARACTERIZE
    /// 
    /// Found 9 solutions in 00:01:46.7221040
    /// </code>
    /// </example>
    ///
    /// <example>
    /// The command <c>WordBrain nwl2018.txt G....T AF..AL PL..LI TO..EE TAM.UR ETO.MB 6 4 8 3 3</c> produces the following output:
    /// <code>
    /// Read 192,111 words in 00:00:00.5641384
    /// 
    /// G . . . . T
    /// A F . . A L
    /// P L . . L I
    /// T O . . E E
    /// T A M . U R
    /// E T O . M B
    /// ______ ____ ________ ___ ___
    /// 
    /// TOMATO FELT UMBRELLA GAP TIE
    /// TOMATO LEFT UMBRELLA GAP TIE
    /// MOTTLE ATOP UMBRELLA FAG TIE
    /// MOTTLE ATOP UMBRELLA TIE FAG
    /// TOMATO FELT UMBRELLA TIE GAP
    /// TOMATO LEFT UMBRELLA TIE GAP
    /// 
    /// Found 6 solutions in 00:01:33.4013404
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
