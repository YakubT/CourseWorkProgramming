using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class EnLanguageField : ILanguageField
    {
        public void UpdateLanguage(FieldViewModel ViewModel)
        {
            const double time = GameStateSingleton.reloadTime;
            ViewModel.LabelContent = "The interval between shots is " + time.ToString() + "s.";
            ViewModel.GoToGameBtnContent = "Resume";
            ViewModel.GoToMenuBtnContent = "Go to menu";
            ViewModel.GoToWindowsBtnContent = "Close game";
        }
    }
}
