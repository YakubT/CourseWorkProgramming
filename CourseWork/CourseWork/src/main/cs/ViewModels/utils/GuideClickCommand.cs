using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class GuideClickCommand : ICommand
    {
        private MainWindowViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public GuideClickCommand(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Guide guide = new Guide();
            guide.Show();
            receiver.Window.Close();
        }
    }
}
