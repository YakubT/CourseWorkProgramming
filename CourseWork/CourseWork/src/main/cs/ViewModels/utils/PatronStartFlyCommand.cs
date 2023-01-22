using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.Models.interfaces;
using CourseWork.src.main.cs.Models.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class PatronStartFlyCommand : ICommand
    {
        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public PatronStartFlyCommand(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            CreatorProxyPatron[] creators = new CreatorProxyPatron[3];
            creators[0] = new CreatorProxyPatron1();
            creators[1] = new CreatorProxyPatron2();
            creators[2] = new CreatorProxyPatron3();
            IPatron patron = creators[receiver.WheelType].Create();
            Image img = new Image();
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Visibility = Visibility.Visible;
            Grid grid = (Grid)receiver.Window.FindName("grid");
            FrameworkElement o = (FrameworkElement)img;
            CanvasUtility.addToGrid(o, grid);
            patron.StartFly(receiver.Angle, img, receiver);
            GameStateSingleton.GetInstance().Reload();
        }
    }
}
