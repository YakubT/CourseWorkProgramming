using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Patron2 : AbstractPatron
    {
        public Patron2()
        {
            demage = 2;
        }
        protected override void SetDisplayProperites()
        {
            width = 0.45;
            height = 2.5;
            img = fieldViewModel.flyWeightSprites[1].GetBitmap;
        }

        protected override void SetStartSpeed(double angle)
        {
            p = 40;
            SetSpeedUsingAngleAndModule(p, angle);
   
        }

        public override void reduceRocket(FieldViewModel link)
        {
            GameStateSingleton.GetInstance().cntRockets[1]--;
            link.Rocket2Cnt = " " + GameStateSingleton.GetInstance().cntRockets[1].ToString();
        }
    }
}
