﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class RestartStrategy0 : AbsrtactRestartStrategy
    {
        protected override void ExecuteLevel(FieldViewModel fieldViewModel)
        {
            fieldViewModel.StartTraining();
        }
    }
}