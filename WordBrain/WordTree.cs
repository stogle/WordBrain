using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WordBrain
{
    public class WordTree
    {
        private readonly string _prefix;
        private readonly IDictionary<string, WordTree> _children = new Dictionary<string, WordTree>(StringComparer.OrdinalIgnoreCase);

        private WordTree(string prefix)
        {
            _prefix = prefix;
        }

        public WordTree(IEnumerable<string> words) : this(string.Empty)
        {
            if (words == null)
            {
                throw new ArgumentNullException(nameof(words));
            }

            foreach (string word in words)
            {
                if (word.Length != 0)
                {
                    Add(word, 0);
                }
            }
        }

        private void Add(string word, int index)
        {
            if (index == word.Length)
            {
                IsWord = true;
                return;
            }

            string c = word.Substring(index, 1);
            if (!_children.TryGetValue(c, out var child))
            {
                child = new WordTree(word.Substring(0, index + 1));
                _children[c] = child;
            }

            child.Add(word, index + 1);
        }

        public bool IsWord { get; private set; }

        public bool TryLetter(char c, ref WordTree childTree)
        {
            if (_children.TryGetValue(c.ToString(CultureInfo.InvariantCulture), out var value))
            {
                childTree = value;
                return true;
            }

            return false;
        }

        public override string ToString() => $"{_prefix}[{string.Concat(_children.Keys)}]";
    }
}
