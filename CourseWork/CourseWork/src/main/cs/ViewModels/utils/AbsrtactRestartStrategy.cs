using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public abstract class AbsrtactRestartStrategy
    {
        protected abstract void ExecuteLevel(FieldViewModel fieldViewModel);

        public void Restart(RestartCommand restartCommand)
        {
            Field window = new Field();
            window.Show();
            restartCommand.Receiver.Window.Close();
            FieldViewModel fieldViewModel = (FieldViewModel)window.DataContext;
            ExecuteLevel(fieldViewModel);
        }
    }
}
