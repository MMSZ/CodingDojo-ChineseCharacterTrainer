using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChineseCharacterTrainer.Library
{
    [ExcludeFromCodeCoverage]
    public class RelayCommand : IAsyncCommand
    {
        readonly Func<object, Task> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Func<object, Task> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Func<object, Task> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute", "DelegateCommand delegates cannot be null");

            _execute = arg => { execute(arg); return Task.Delay(0); };
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public async Task ExecuteAsync(object parameter)
        {
            await _execute(parameter);
        }
    }

    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}