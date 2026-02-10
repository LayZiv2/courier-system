using System;
using System.Windows.Input;

namespace CourierSystemWPF.Utilities
{
    class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;

        /// <summary>
        /// Occurs when changes affect whether the command can execute.
        /// </summary>
        /// <remarks>This event is typically raised by the <see cref="CommandManager"/> to indicate that
        /// the  execution status of the command may have changed. Handlers added to this event should  check the
        /// command's ability to execute by calling the <c>CanExecute</c> method.</remarks>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Represents a command that can be executed in response to user interaction, with optional logic to determine
        /// if the command can execute.
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked. This parameter cannot be <see langword="null"/>.</param>
        /// <param name="canExecute">An optional function that determines whether the command can execute. If <see langword="null"/>, the command
        /// is always executable.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <see langword="null"/>.</exception>
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">An optional parameter used by the command to evaluate its execution state. This can be <see
        /// langword="null"/> if the command does not require a parameter.</param>
        /// <returns><see langword="true"/> if the command can execute; otherwise, <see langword="false"/>.</returns>
        public bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);

        
        public void Execute(object? parameter) => _execute(parameter);
    }
}
