using System;
using System.Linq;

namespace WordBrain
{
    public class Solution
    {
        private readonly int _length;
        private readonly int[] _lengths;
        private readonly string?[] _words;

        public Solution(int[] lengths)
        {
            if (lengths == null)
            {
                throw new ArgumentNullException(nameof(lengths));
            }
            if (lengths.Any(length => length <= 0))
            {
                throw new ArgumentException(Strings.Solution_ExpectedPositiveLengthsExceptionMessage, nameof(lengths));
            }

            _length = lengths.Length;
            _lengths = lengths;
            _words = new string?[_length];
            IsComplete = _length == 0;
            RemainingLetters = _lengths.Sum();
        }

        private Solution(int[] lengths, string?[] words)
        {
            _length = lengths.Length;
            _lengths = lengths;
            _words = words;
            IsComplete = _words.All(word => word != null);
            RemainingLetters = _lengths.Zip(_words, (length, word) => word == null ? length : 0).Sum();
        }

        public bool IsComplete { get; }

        public int RemainingLetters { get; }

        internal bool TryPlay(string word, out Solution? solution)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_lengths[i] == word.Length && _words[i] == null)
                {
                    int[] lengths = _lengths.ToArray();
                    string?[] words = _words.ToArray();
                    words[i] = word;
                    solution = new Solution(lengths, words);
                    return true;
                }
            }

            solution = null;
            return false;
        }

        public override string ToString() => string.Join(' ', _words.Zip(_lengths, (word, length) => word ?? new string('_', length)));
    }
}
