using System;
using System.Drawing;
using CourseWork.src.main.cs.ViewModels.intefaces;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainEnglishLanguageImplementor : IMainViewModelLanguageState
    {
        public void UpdateLanguage(MainWindowViewModel mainWindowViewModel)
        {
            mainWindowViewModel.ColorOfEnLabel = ColorTranslator.ToHtml(Color.GreenYellow);
            mainWindowViewModel.ColorOfUkrLabel = ColorTranslator.ToHtml(Color.White);
            mainWindowViewModel.ButtonGuideText = "Guide";
            mainWindowViewModel.ButtonPlayText = "Play";
        }
    }
}
