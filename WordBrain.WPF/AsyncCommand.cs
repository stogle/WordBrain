using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordBrain.WPF
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<object, Task> _executeAsync;
        private readonly Predicate<object>? _canExecute;
        private readonly bool _canExecuteInParallel;

        public AsyncCommand(Func<object, Task> executeAsync, Predicate<object>? canExecute = null, bool canExecuteInParallel = false)
        {
            _executeAsync = executeAsync;
            _canExecute = canExecute;
            _canExecuteInParallel = canExecuteInParallel;
        }

        public Task ExecuteAsync(object parameter)
        {
            return _executeAsync(parameter);
        }

        public bool IsExecuting { get; private set; }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private static void InvokeCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter) => (!IsExecuting || _canExecuteInParallel) && (_canExecute?.Invoke(parameter) ?? true);

        public async void Execute(object parameter)
        {
            try
            {
                IsExecuting = true;
                await ExecuteAsync(parameter).ConfigureAwait(true);
            }
            finally
            {
                IsExecuting = false;
            }

            InvokeCanExecuteChanged();
        }
    }
}
