using System.Collections.Generic;
using System.Linq;

namespace WordBrain
{
    public class Solution
    {
        private readonly int _length;
        private readonly IReadOnlyList<int> _lengths;
        private readonly Sequence?[] _sequences;

        internal Solution(IReadOnlyList<int> lengths)
        {
            _length = lengths.Count;
            _lengths = lengths;
            _sequences = new Sequence?[_length];
            IsComplete = _length == 0;
        }

        private Solution(IReadOnlyList<int> lengths, Sequence?[] sequences)
        {
            _length = lengths.Count;
            _lengths = lengths;
            _sequences = sequences;
            IsComplete = _sequences.All(word => word != null);
        }

        public bool IsComplete { get; }

        internal bool TryPlay(Sequence sequence, out Solution? solution)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_lengths[i] == sequence.Length && _sequences[i] == null)
                {
                    Sequence?[] sequences = _sequences.ToArray();
                    sequences[i] = sequence;
                    solution = new Solution(_lengths, sequences);
                    return true;
                }
            }

            solution = null;
            return false;
        }

        public IEnumerable<string> Words => _sequences.Zip(_lengths, (sequence, length) => sequence?.ToString() ?? new string('_', length));

        public override string ToString() => string.Join(' ', Words);
    }
}
