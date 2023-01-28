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

        private AbsrtactRestartState[] restartState;
        public RestartCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
            restartState = new AbsrtactRestartState[2];
            restartState[0] = new RestartState0();
            restartState[1] = new RestartState1();
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
