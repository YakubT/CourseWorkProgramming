using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class Level2Click : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ChooseLevelViewModel receiver;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public Level2Click(ChooseLevelViewModel receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(object parameter)
        {
            Field fieldView = new Field();
            fieldView.Show();
            FieldViewModel fieldViewModel = (FieldViewModel)fieldView.DataContext;
            fieldViewModel.StartLevel2();
            receiver.Window.Close();
        }
    }
}
