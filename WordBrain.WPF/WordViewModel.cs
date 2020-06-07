using System.Collections.Generic;
using System.Linq;

namespace WordBrain.WPF
{
    public class WordViewModel : ViewModel
    {
        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                if (SetValue(ref _length, value))
                {
                    Value = new char?[value];
                }
            }
        }

        private IEnumerable<char?> _value = Enumerable.Empty<char?>();
        public IEnumerable<char?> Value
        {
            get => _value;
            private set => SetValue(ref _value, value);
        }
    }
}
