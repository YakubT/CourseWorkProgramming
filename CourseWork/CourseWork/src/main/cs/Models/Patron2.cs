using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Patron2 : Patron
    {
        public override void SetDisplayProperites()
        {
            width = 0.5;
            height = 3;
            img = new BitmapImage(new Uri("/src/main/resources/rockets/ppo_rocket2.png", UriKind.Relative));
        }

        public override void SetStartSpeed(double angle)
        {
            p = 18;
            SetSpeedUsingAngleAndModule(p, angle);
   
        }
    }
}
