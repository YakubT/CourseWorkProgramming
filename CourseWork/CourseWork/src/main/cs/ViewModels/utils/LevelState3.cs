using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class LevelState3 : ILevelState
    {
        public void setStateOfButtons(ChooseLevelViewModel viewModel)
        {
            viewModel.IsEnabledLevel2 = true;
            viewModel.IsEnabledLevel3 = true;
            viewModel.Level1PassedVisibility = "Visible";
            viewModel.Level2PassedVisibility = "Visible";
            viewModel.Level3PassedVisibility = "Visible";
        }
    }
}
