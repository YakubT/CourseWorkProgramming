using CourseWork.src.main.cs.Models.interfaces;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public class ProxyPatron1 : IPatron
    {
        private Patron1 patron1;
        public void StartFly(double angle, Image img, FieldViewModel link)
        {
            if (patron1==null)
            {
                patron1 = new Patron1();
            }
            if (GameStateSingleton.GetInstance().cntRockets[0]>0 && GameStateSingleton.GetInstance().IsReloaded)
            {
                patron1.StartFly(angle,img,link);
            }
            else
            {
                if (GameStateSingleton.GetInstance().cntRockets[0] == 0)
                {
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    link.AlertVisibility = "Visible";
                    timer.Tick += (o, s) =>
                    {
                        link.AlertVisibility = "Hidden";
                        timer.Stop();
                    };
                    timer.Start();
                }
            }
        }
    }
}
