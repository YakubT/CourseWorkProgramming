using CourseWork.src.main.cs.Models.interfaces;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseWork.src.main.cs.Models
{
    public class ProxyPatron2 : IPatron
    {
        private Patron2 patron2;
        public void StartFly(double angle, Image img, FieldViewModel link)
        {
            if (patron2 == null)
            {
                patron2 = new Patron2();
            }
            if (GameStateSingleton.GetInstance().cntRockets[0] > 0 && GameStateSingleton.GetInstance().IsReloaded)
            {
                patron2.StartFly(angle, img, link);
            }
        }
    }
}
