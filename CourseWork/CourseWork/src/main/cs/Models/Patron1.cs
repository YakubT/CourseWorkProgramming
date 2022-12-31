using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Patron1 : AbstractPatron
    {
        protected override void SetDisplayProperites()
        {
            width = 0.5;
            height = 3;
            img = new BitmapImage(new Uri("/src/main/resources/rockets/ppo_rocket1.png", UriKind.Relative));
            
        }
        protected override void SetStartSpeed(double angle)
        {
            p = 40;
            SetSpeedUsingAngleAndModule(p, angle);
        }
    }
}
