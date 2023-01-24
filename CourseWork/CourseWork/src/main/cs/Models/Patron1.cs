using CourseWork.src.main.cs.ViewModels;
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
        public Patron1()
        {
            demage = 1;
        }
        protected override void SetDisplayProperites()
        {
            width = 0.15;
            height = 2;
            img = fieldViewModel.flyWeightSprites[0].GetBitmap;
            
        }
        protected override void SetStartSpeed(double angle)
        {
            p = 40;
            SetSpeedUsingAngleAndModule(p, angle);
        }

        public override void reduceRocket(FieldViewModel link)
        {
            GameStateSingleton.GetInstance().cntRockets[0]--;
            link.Rocket1Cnt =" "+GameStateSingleton.GetInstance().cntRockets[0].ToString();
        }
    }
}
