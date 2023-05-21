using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class ChooseLevelEnglishLanguageImplementor : ILanguageChooseLevelStrategy
    {
        public void UpdateLanguage(ChooseLevelViewModel ViewModel)
        {
            ViewModel.TitleText = "Levels";
            ViewModel.BackButtonContent = "Back";
            ViewModel.TrainingText = "Training";
        }
    }
}
