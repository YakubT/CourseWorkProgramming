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
    public class ProxyPatron3 : IPatron
    {
        private Patron3 patron3;
        public void StartFly(double angle, Image img, FieldViewModel link)
        {
            if (patron3 == null)
            {
                patron3 = new Patron3();
            }
            if (GameStateSingleton.GetInstance().cntRockets[0] > 0 && GameStateSingleton.GetInstance().IsReloaded)
            {
                patron3.StartFly(angle, img, link);
            }
        }
    }
}
