using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class LeftFlyDirectionState : FlyDirectionState
    {
        public void StartFlyPreprocessing(AbstractPlain plain)
        {
            plain.PlainPutLeft();
            plain.Coordinates.Y = 19;
            plain.sprite = plain.sprite1;
        }
    }
}
