using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class ResumeCommand : ICommand
    {
        private FieldViewModel receiver; 

        public event EventHandler CanExecuteChanged;


        public ResumeCommand (FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            receiver.Resume();
            receiver.PauseMenuVisibility = "Hidden";

        }
    }
}
