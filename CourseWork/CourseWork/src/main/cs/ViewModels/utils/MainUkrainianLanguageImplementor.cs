using CourseWork.src.main.cs.ViewModels.intefaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainUkrainianLanguageImplementor : IMainViewModelLanguageState
    {
        public void UpdateLanguage(MainWindowViewModel mainWindowViewModel)
        {
            mainWindowViewModel.ColorOfUkrLabel = ColorTranslator.ToHtml(Color.GreenYellow);
            mainWindowViewModel.ColorOfEnLabel = ColorTranslator.ToHtml(Color.White);
            mainWindowViewModel.ButtonGuideText = "Інструкція";
            mainWindowViewModel.ButtonPlayText = "Грати";
        }
    }
}
