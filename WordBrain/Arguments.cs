using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WordBrain
{
    public class Arguments
    {
        private readonly string[] _args;
        private readonly string _args0;

        public Arguments(string[] args, string args0)
        {
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _args0 = args0 ?? throw new ArgumentNullException(nameof(args0));
        }

        public bool TryParse([NotNullWhen(true)]out string? path, [NotNullWhen(true)]out char?[][]? letters, [NotNullWhen(true)]out int[]? lengths)
        {
            int argsIndex = 0;
            if (!TryParsePath(ref argsIndex, out path))
            {
                letters = null;
                lengths = null;
                return false;
            }
            if (!TryParseLetters(ref argsIndex, out letters))
            {
                lengths = null;
                return false;
            }
            if (!TryParseLengths(ref argsIndex, out lengths))
            {
                return false;
            }

            return true;
        }

        private bool TryParsePath(ref int argsIndex, [NotNullWhen(true)]out string? path)
        {
            if (argsIndex >= _args.Length)
            {
                path = null;
                return false;
            }

            path = _args[argsIndex++];
            return true;
        }

        private bool TryParseLetters(ref int argsIndex, [NotNullWhen(true)]out char?[][]? letters)
        {
            if (argsIndex >= _args.Length)
            {
                letters = null;
                return false;
            }

            int width = _args[argsIndex].Length;

            var lettersList = new List<char?[]>();
            while (argsIndex < _args.Length && !int.TryParse(_args[argsIndex], out _))
            {
                string line = _args[argsIndex++];
                if (line.Length != width)
                {
                    letters = null;
                    return false;
                }

                lettersList.Add(line.Select(letter => letter == '.' ? (char?)null : letter).ToArray());
            }
            letters = lettersList.ToArray();
            return true;
        }

        private bool TryParseLengths(ref int argsIndex, [NotNullWhen(true)]out int[]? lengths)
        {
            int lengthsCount = _args.Length - argsIndex;
            if (lengthsCount <= 0)
            {
                lengths = null;
                return false;
            }

            lengths = new int[lengthsCount];
            for (int i = 0; i < lengthsCount; i++)
            {
                if (!int.TryParse(_args[argsIndex++], out int length))
                {
                    lengths = null;
                    return false;
                }
                lengths[i] = length;
            }
            return true;
        }

        public string Usage => string.Format(Strings.Culture, Strings.Arguments_UsageFormat, _args0, Environment.NewLine);
    }
}
