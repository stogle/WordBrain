using System;
using System.Diagnostics;
using System.IO;

namespace WordBrain
{
    /// <example>
    /// The command <c>WordBrain CSW19.txt SLLY HAUE ICTN PAON 4 8 4</c> produces the following output:
    /// <code>
    /// S L L Y
    /// H A U E
    /// I C T N
    /// P A O N
    /// ____ ________ ____
    /// 
    /// SHIP ACTUALLY NONE
    /// NONE ACTUALLY SHIP
    /// 
    /// Found 2 solutions in 00:00:00.8331733
    /// </code>
    /// </example>
    /// <example>
    /// The command <c>WordBrain CSW19.txt EZEAI UICRD LRHAD AEAEO VCTES 5 4 4 12</c> produces the following output:
    /// <code>
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
    /// Found 11 solutions in 00:28:26.4260580
    /// </code>
    /// </example>
    /// <example>
    /// The command <c>WordBrain CSW19.txt EZE.. UICA. LRHR. AEAAI VCTED 5 4 12</c> produces the following output:
    /// <code>
    /// E Z E. .
    /// U I C A.
    /// L R H R.
    /// A E A A I
    /// V C T E D
    /// _____ ____ ____________
    /// 
    /// VALUE AIDE CHARACTERIZE
    /// VALUE IDEA CHARACTERIZE
    /// 
    /// Found 2 solutions in 00:00:08.5289698
    /// </code>
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
            Console.WriteLine(Strings.Program_FoundSolutionsFormat, count, stopWatch.Elapsed);
        }
    }
}
