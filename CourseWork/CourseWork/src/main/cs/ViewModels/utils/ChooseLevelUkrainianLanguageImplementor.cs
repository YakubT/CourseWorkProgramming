using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class ChooseLevelUkrainianLanguageImplementor : ILanguageChooseLevelState
    {
        public void UpdateLanguage(ChooseLevelViewModel ViewModel)
        {
            ViewModel.TitleText = "Рівні";
            ViewModel.BackButtonContent = "Назад";
            ViewModel.TrainingText = "Тренування";
        }
    }
}
