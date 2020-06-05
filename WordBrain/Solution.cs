﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WordBrain
{
    public class Solution
    {
        private readonly IReadOnlyList<Word> _items;
        private readonly int _index;
        private readonly int _length;

        internal Solution(IReadOnlyList<int> lengths)
        {
            _items = lengths.Select(length => new Word(length)).ToArray();
            _index = 0;
            _length = lengths.Count;
            MaxLength = lengths.Cast<int?>().Max();
        }

        private Solution(IReadOnlyList<Word> items, int index)
        {
            _items = items;
            _index = index;
            _length = items.Count;
            MaxLength = _items.Skip(index).Select(item => item.Length).Max();
        }

        public int? MaxLength { get; }

        internal bool TryPlay(Sequence sequence, [NotNullWhen(true)]out Solution? solution)
        {
            for (int i = _index; i < _length; i++)
            {
                if (_items[i].Length == sequence.Length)
                {
                    Word[] items = _items.ToArray();
                    items[i] = items[_index];
                    items[_index] = new Word(sequence);
                    solution = new Solution(items, _index + 1);
                    return true;
                }
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
