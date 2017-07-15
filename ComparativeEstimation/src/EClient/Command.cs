using System;
using System.Windows.Input;

namespace EClient
{
    public class Command : ICommand
    {
        private readonly Action execute;

        public Command(Action execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
