using System;
using System.Windows.Input;

namespace RoutePlanner.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> action;
        public DelegateCommand(Action<T> action)
        {
            this.action = action;
        }

        private bool canExecute = false;
        public void UpdateCanExecute(bool canExecute)
        {
            this.canExecute = canExecute;
            CanExecuteChanged(this,EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute;    
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public void Execute(object parameter)
        {
            action((T)parameter);
        }
    }
}