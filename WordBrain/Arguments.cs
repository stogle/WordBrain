using System;
using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Arguments
    {
        public Arguments(string[] args, string args0)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            if (args0 == null)
            {
                throw new ArgumentNullException(nameof(args0));
            }

            int argsIndex = 0;
            Path = ParsePath(args, ref argsIndex);
            char?[][]? letters = ParseLetters(args, ref argsIndex);
            if (letters != null)
            {
                int[]? lengths = ParseLengths(args, ref argsIndex);
                if (lengths != null)
                {
                    Puzzle = new Puzzle(letters, lengths);
                }
            }
            IsValid = Path != null && Puzzle != null;
            Usage = string.Format(Strings.Culture, Strings.Arguments_UsageFormat, args0, Environment.NewLine);
        }

        private static string? ParsePath(string[] args, ref int argsIndex) => argsIndex < args.Length ? args[argsIndex++] : null;

        private static char?[][]? ParseLetters(string[] args, ref int argsIndex)
        {
            if (argsIndex >= args.Length)
            {
                return null;
            }

            int width = args[argsIndex].Length;

            var letters = new List<char?[]>();
            while (argsIndex < args.Length && !int.TryParse(args[argsIndex], out _))
            {
                string line = args[argsIndex++];
                if (line.Length != width)
                {
                    return null;
                }

                letters.Add(line.Select(letter => letter == '.' ? (char?)null : letter).ToArray());
            }
            return letters.ToArray();
        }

        private static int[]? ParseLengths(string[] args, ref int argsIndex)
        {
            int lengthsCount = args.Length - argsIndex;
            if (lengthsCount <= 0)
            {
                return null;
            }

            int[] lengths = new int[lengthsCount];
            for (int i = 0; i < lengthsCount; i++)
            {
                if (!int.TryParse(args[argsIndex++], out int length))
                {
                    return null;
                }
                lengths[i] = length;
            }
            return lengths;
        }

        public string? Path { get; }

        public Puzzle? Puzzle { get; }

        public bool IsValid { get; }

        public string Usage { get; }
    }
}
