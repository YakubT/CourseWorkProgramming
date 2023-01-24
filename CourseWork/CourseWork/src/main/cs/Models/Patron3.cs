using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Patron3 : AbstractPatron
    {
        public Patron3()
        {
            demage = 3;
        }
        protected override void SetDisplayProperites()
        {
            width = 0.45;
            height = 2.5;
            img = fieldViewModel.flyWeightSprites[2].GetBitmap;
        }

        protected override void SetStartSpeed(double angle)
        {
            p = 30;
            SetSpeedUsingAngleAndModule(p, angle);
   
        }

        public override void reduceRocket(FieldViewModel link)
        {
            GameStateSingleton.GetInstance().cntRockets[2]--;
            link.Rocket3Cnt = " " + GameStateSingleton.GetInstance().cntRockets[2].ToString();
        }
    }
}
