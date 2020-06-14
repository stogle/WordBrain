namespace WordBrain.WPF
{
    public class LineViewModel : ViewModel
    {
        private string _text = string.Empty;
        public string Text
        {
            get => _text;
            set => SetValue(ref _text, value?.ToUpperInvariant() ?? string.Empty);
        }
    }
}
