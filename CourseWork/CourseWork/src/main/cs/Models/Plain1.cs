﻿using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Plain1 : AbstractPlain
    {
        public Plain1(FieldViewModel viewModel) : base(viewModel) { health = 20; }
        protected override void SetSize()
        {
            width = 3;
            height = 1.2;
            maxHealth = health = 15;
        }

        protected override void SetSpeed()
        {
            speed = 6.2;
        }
    }
}
