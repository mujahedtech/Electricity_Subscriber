using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Electricity_Subscriber.CL
{
    public class RelayCommand : ICommand
    {

        Action<object> _execteMethod;
        Func<object, bool> _canexecuteMethod;

        public RelayCommand(Action<object> execteMethod)
        {
            _execteMethod = execteMethod;

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execteMethod(parameter);
        }
    }
}
