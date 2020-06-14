using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordBrain.WPF
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Grid.CollectionChanged += GridCollectionChanged;
            Grid.Add(new LineViewModel());
            Solve = new AsyncCommand(ExecuteSolveAsync, CanExecuteSolve);
        }

        public ObservableCollection<LineViewModel> Grid { get; } = new ObservableCollection<LineViewModel>();

        private void GridCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var line in e.OldItems.OfType<LineViewModel>())
                {
                    line.PropertyChanged -= LinePropertyChanged;
                }
            }
            if (e.NewItems != null)
            {
                foreach (var line in e.NewItems.OfType<LineViewModel>())
                {
                    line.PropertyChanged += LinePropertyChanged;
                }
            }
            Rows = Grid.Count - 1;
        }

        private void LinePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = (LineViewModel)sender;
            bool isLast = item == Grid.Last();
            if (isLast && item.Text.Length != 0)
            {
                Grid.Add(new LineViewModel());
            }
            else if (!isLast && item.Text.Length == 0)
            {
                Grid.Remove(item);
            }

            int width = Grid.Max(line => line.Text.Length);
            Letters = Grid.Take(Rows)
                          .Select(line => line.Text.PadRight(width))
                          .SelectMany(s => s)
                          .Select(c => char.IsLetter(c) ? c : (char?)null)
                          .DefaultIfEmpty('?')
                          .ToList();
        }

        private int _rows;
        public int Rows
        {
            get => _rows;
            private set => SetValue(ref _rows, value);
        }

        private IReadOnlyCollection<char?> _letters = new List<char?> { '?' };
        public IReadOnlyCollection<char?> Letters
        {
            get => _letters;
            private set
            {
                if (SetValue(ref _letters, value))
                {
                    AdjustSolutionToMatchLetters();
                }
            }
        }

        private void AdjustSolutionToMatchLetters(WordViewModel? vmToSkip = null)
        {
            int lettersCount = Letters.Count(c => c != null);

            // Remove words and reduce lengths
            int i = 0;
            int total = 0;
            while (i < Solution.Count)
            {
                var vm = Solution[i];
                if (total < lettersCount)
                {
                    if (total + vm.Length > lettersCount)
                    {
                        vm.PropertyChanged -= WordViewModelPropertyChanged;
                        vm.Length = lettersCount - total;
                        vm.PropertyChanged += WordViewModelPropertyChanged;
                    }
                    total += vm.Length;
                    i++;
                }
                else
                {
                    vm.PropertyChanged -= WordViewModelPropertyChanged;
                    Solution.RemoveAt(i);
                }
            }

            // Add word or increase length
            if (total < lettersCount)
            {
                var vm = Solution.LastOrDefault();
                if (vm == null || vm == vmToSkip)
                {
                    vm = new WordViewModel { Length = lettersCount - total };
                    vm.PropertyChanged += WordViewModelPropertyChanged;
                    Solution.Add(vm);
                }
                else
                {
                    vm.PropertyChanged -= WordViewModelPropertyChanged;
                    vm.Length += lettersCount - total;
                    vm.PropertyChanged += WordViewModelPropertyChanged;
                }
            }
        }

        private void WordViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) => AdjustSolutionToMatchLetters(sender as WordViewModel);

        public ObservableCollection<WordViewModel> Solution { get; } = new ObservableCollection<WordViewModel> { new WordViewModel { Length = 1 } };

        public ICommand Solve { get; }

        private async Task ExecuteSolveAsync(object _)
        {
            var wordTree = new WordTree(System.IO.File.ReadAllLines("3of6game.txt"));
            var solver = new Solver(wordTree);
            var letters = Grid.Take(Rows).Select(line => line.Text.Select(c => char.IsLetter(c) ? c : (char?)null).ToArray()).ToArray();
            var puzzle = new Puzzle(letters, Solution.Select(vm => vm.Length).ToArray());
            Output.Clear();
            await foreach (var solution in solver.SolveAsync(puzzle))
            {
                Output.Add(solution);
            }
        }

        private bool CanExecuteSolve(object _) => Rows != 0;

        public ObservableCollection<Solution> Output { get; } = new ObservableCollection<Solution>();
    }
}
