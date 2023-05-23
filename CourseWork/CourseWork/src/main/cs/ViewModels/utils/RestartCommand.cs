using CourseWork.src.main.cs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class RestartCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private FieldViewModel receiver;

        public FieldViewModel Receiver { get => receiver; }

        private AbsrtactRestartStrategy[] restartState;
        public RestartCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
            restartState = new AbsrtactRestartStrategy[4];
            restartState[0] = new RestartStrategy0();
            restartState[1] = new RestartStrategy1();
            restartState[2] = new RestartStrategy2();
            restartState[3] = new RestartStrategy3();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            int level = GameStateSingleton.GetInstance().levelLoaded;
            restartState[level].Restart(this);
        }
    }
}
