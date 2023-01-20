using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class BackButtonClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ICloseableWindow receiver;

        public BackButtonClickCommand(ICloseableWindow receiver)
        {
            this.receiver = receiver;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            receiver.Window.Close();
        }
    }
}
