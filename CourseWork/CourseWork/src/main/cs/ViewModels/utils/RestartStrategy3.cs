using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CourseWork.src.main.cs.ViewModels.utils;

namespace CourseWork.src.main.cs.ViewModels
{
    public class RestartStrategy3 : AbsrtactRestartStrategy
    {
        protected override void ExecuteLevel(FieldViewModel fieldViewModel)
        {
            fieldViewModel.StartLevel3();
        }
    }
}
