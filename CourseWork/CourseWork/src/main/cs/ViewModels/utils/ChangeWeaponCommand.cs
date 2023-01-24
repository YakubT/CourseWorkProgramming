using CourseWork.src.main.cs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{

    public class ChangeWeaponCommand : ICommand
    {
        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public ChangeWeaponCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            if (GameStateSingleton.GetInstance().Ispause)
                return;
            try
            {
                Dictionary<Key, int> dictionary = new Dictionary<Key, int>();
                KeyEventArgs keyArgs = (KeyEventArgs) parameter;
                dictionary[Key.D1] = 0;
                dictionary[Key.D2] = 1;
                dictionary[Key.D3] = 2;
                if (keyArgs.Key>=Key.D1 && keyArgs.Key <=Key.D3)
                {
                    receiver.WheelType = (uint)dictionary[keyArgs.Key];
                }
            }
            catch (Exception e)
            {
                receiver.WheelType = (receiver.WheelType + 1) % 3;
            }
        }
    }
}
