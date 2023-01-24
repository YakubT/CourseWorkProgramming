using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class Level1Click : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ChooseLevelViewModel receiver;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public Level1Click(ChooseLevelViewModel receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(object parameter)
        {
            Field fieldView = new Field();
            fieldView.Show();
            FieldViewModel fieldViewModel = (FieldViewModel)fieldView.DataContext;
            fieldViewModel.StartLevel1();
            receiver.Window.Close();
        }
    }
}
