using CourseWork.src.main.cs.ViewModels.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class EnglishLanguageGuide : IGuideChangeLanguageStrategy
    {
        public void UpdateLanguage(GuideViewModel ViewModel)
        {
            ViewModel.BackButtonContent = "Back";
        }
    }
}
