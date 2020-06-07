using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordBrain.WPF
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Solve = new AsyncCommand(ExecuteSolveAsync, CanExecuteSolve);
        }

        private string _grid = string.Empty;
        public string Grid
        {
            get => _grid;
            set
            {
                value = (value ?? string.Empty).ToUpper(CultureInfo.CurrentCulture);
                if (SetValue(ref _grid, value))
                {
                    Letters = value.Select(c => char.IsLetter(c) ? c : (char?)null).ToList();
                }
            }
        }

        private IReadOnlyCollection<char?> _letters = new List<char?> { '?' };
        public IReadOnlyCollection<char?> Letters
        {
            get => _letters;
            private set
            {
                if (!value.Any())
                {
                    value = new List<char?> { '?' };
                }
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
            int i = 0, lettersPerRow = (int)Math.Sqrt(Letters.Count);
            var letters = Letters.GroupBy(letter => i++ / lettersPerRow).Select(group => group.ToArray()).ToArray();
            var puzzle = new Puzzle(letters, Solution.Select(vm => vm.Length).ToArray());
            Output.Clear();
            await foreach (var solution in solver.SolveAsync(puzzle))
            {
                Output.Add(solution);
            }
        }

        private bool CanExecuteSolve(object _)
        {
            int length = Grid.Length;
            int sqrtLength = (int)Math.Sqrt(length);
            return length != 0 && sqrtLength * sqrtLength == length;
        }

        public ObservableCollection<Solution> Output { get; } = new ObservableCollection<Solution>();
    }
}
