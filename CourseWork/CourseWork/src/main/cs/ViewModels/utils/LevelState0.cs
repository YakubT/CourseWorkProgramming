using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class LevelState0 : ILevelState
    {
        public void setStateOfButtons(ChooseLevelViewModel viewModel)
        {
            viewModel.IsEnabledLevel2 = false;
            viewModel.IsEnabledLevel3 = false;
            viewModel.Level1PassedVisibility = viewModel.Level2PassedVisibility = viewModel.Level3PassedVisibility = "Hidden";
        }
    }
}
