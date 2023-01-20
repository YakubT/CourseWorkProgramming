using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels
{
    public class PlayClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private MainWindowViewModel receiver;

        public PlayClickCommand(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ChooseLevel chooseLevel = new ChooseLevel();
            chooseLevel.Show();
            receiver.Window.Close();
        }
    }
}
