using CourseWork.src.main.cs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class PauseCommand : ICommand
    {

        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public PauseCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (((KeyEventArgs)parameter).Key.Equals(Key.Escape))
            {
                receiver.Pause();
                receiver.PauseMenuVisibility = "Visible";
            }
           
        }
    }
}
