﻿using System;
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
            demage = 15;
        }
        protected override void SetDisplayProperites()
        {
            width = 0.45;
            height = 2.5;
            img = new BitmapImage(new Uri("/src/main/resources/rockets/ppo_rocket2.png", UriKind.Relative));
        }

        protected override void SetStartSpeed(double angle)
        {
            p = 20;
            SetSpeedUsingAngleAndModule(p, angle);
   
        }
    }
}
