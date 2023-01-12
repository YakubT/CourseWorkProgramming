using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class RightFlyDirectionState : FlyDirectionState
    {
        public void StartFlyPreprocessing(Enemy plain)
        {
            plain.AntiSpeed();
            plain.PlainPutRight();
            plain.sprite = plain.spriteMirror;
        }
    }
}
