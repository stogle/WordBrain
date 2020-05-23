using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Solution
    {
        private readonly int _length;
        private readonly IReadOnlyList<int> _lengths;
        private readonly string?[] _words;

        internal Solution(IReadOnlyList<int> lengths)
        {
            _length = lengths.Count;
            _lengths = lengths;
            _words = new string?[_length];
            IsComplete = _length == 0;
        }

        private Solution(IReadOnlyList<int> lengths, string?[] words)
        {
            _length = lengths.Count;
            _lengths = lengths;
            _words = words;
            IsComplete = _words.All(word => word != null);
        }

        public bool IsComplete { get; }

        internal bool TryPlay(string word, out Solution? solution)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_lengths[i] == word.Length && _words[i] == null)
                {
                    string?[] words = _words.ToArray();
                    words[i] = word;
                    solution = new Solution(_lengths, words);
                    return true;
                }
            }

            solution = null;
            return false;
        }

        public override string ToString() => string.Join(' ', _words.Zip(_lengths, (word, length) => word ?? new string('_', length)));
    }
}
