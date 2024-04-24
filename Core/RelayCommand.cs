using System;
using System.Windows.Input;

namespace WpfApp.Core
{
    internal class RelayCommend : ICommand
    {
        private Action<Object> _execute ;
        private Func<Object,bool> _canExecute; // <T>
        public event EventHandler CanExecuteChanged
        {
            add { 
                CommandManager.RequerySuggested += value; 
            }
            remove { 
                CommandManager.RequerySuggested -= value; 
            }
            
        }
        public RelayCommend(Action <Object> execute,Func <Object,bool> canExcecute = null)
        {
            execute = null;
            _execute = execute;
            _canExecute = canExcecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
