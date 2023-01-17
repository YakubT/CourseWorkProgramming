using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels
{
    public class GuideViewModel : BaseViewModel
    {
        private string backButtonContent;

        private Window window;

        public Window Window { get=>window; }

        public ICommand BackButtonClick{get;}
        public string BackButtonContent
        {
            get => backButtonContent;
            set
            {
                backButtonContent = value;
                OnPropertyChanged(backButtonContent);
            }
        }

        public GuideViewModel(Window window)
        {
            this.window = window;
            BackButtonContent = ConfigurationManager.AppSettings["language"]=="UA"?"Назад":"Back";
            BackButtonClick = new BackButtonClick(this);
        }
    }

    public class BackButtonClick : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private GuideViewModel receiver;

        public BackButtonClick(GuideViewModel receiver)
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
