using CourseWork.src.main.cs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class GunRotateCommand : ICommand
    {
        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public GunRotateCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            if (GameStateSingleton.GetInstance().Ispause)
                return;

            double xGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunX"]);
            double yGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunY"]);
            double heightGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunHeight"]);
            double tg = ((((MouseEventArgs)parameter).GetPosition(receiver.Window).Y -
                receiver.Window.ActualHeight * (24 - yGun) / 24) /
                (((MouseEventArgs)parameter).GetPosition(receiver.Window).X - receiver.Window.ActualWidth * xGun / 24.0));
            double angle = 180 * Math.Atan(Math.Abs(tg)) / Math.PI;
            if (tg > 0)
            {
                angle = -90 + angle;
            }
            else
                angle = 90 - angle;
            if (angle > -60 && angle < 50)
                this.receiver.Angle = angle;

        }
    }
}
