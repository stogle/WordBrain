using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WordBrain
{
    public class Solution
    {
        private readonly IReadOnlyList<Word> _items;
        private readonly int _index;

        internal Solution(IReadOnlyList<int> lengths)
        {
            _items = lengths.Select(length => new Word(length)).ToArray();
            _index = 0;
            NextLength = _items.Any() ? _items[0].Length : null;
        }

        private Solution(IReadOnlyList<Word> items, int index)
        {
            _items = items;
            _index = index;
            NextLength = index < _items.Count ? _items[index].Length : null;
        }

        public int? NextLength { get; }

        internal bool TryPlay(Sequence sequence, [NotNullWhen(true)]out Solution? solution)
        {
            if (sequence.Length == NextLength)
            {
                Word[] items = _items.ToArray();
                items[_index] = new Word(sequence);
                solution = new Solution(items, _index + 1);
                return true;
            }

            solution = null;
            return false;
        }

        public IEnumerable<string> Words => _items.Select(word => word.ToString());

        public override string ToString() => string.Join(' ', Words);

        private class Word
        {
            public readonly int? Length;
            public readonly Sequence? Sequence;

            public Word(int length) => Length = length;
            public Word(Sequence sequence) => Sequence = sequence;

            public override string ToString() => Sequence?.ToString() ?? new string('_', Length!.Value);
        }
    }
}
