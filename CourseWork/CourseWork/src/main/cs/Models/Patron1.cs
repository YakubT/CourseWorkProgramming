using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Patron1 : Patron
    {
        static Patron1()
        {
            p = 30;
        }
        public override void SetDisplayProperites()
        {
            width = 0.5;
            height = 3;
            img = new BitmapImage(new Uri("/src/main/resources/ppo_rocket1.png", UriKind.Relative));
        }
        public override void SetStartSpeed(double angle)
        {
            SetSpeedUsingAngleAndModule(p, angle);
        }
    }
}
