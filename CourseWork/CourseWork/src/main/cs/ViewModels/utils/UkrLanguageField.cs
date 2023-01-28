using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.utility;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.utils
{
    public class UkrLanguageField : ILanguageField
    {
        public void UpdateLanguage(FieldViewModel ViewModel)
        {
            const double time = GameStateSingleton.reloadTime;
            ViewModel.LabelContent = "Інтервал між пострілами - " + time.ToString() + "с.";
            ViewModel.GoToGameBtnContent = "Назад до гри";
            ViewModel.GoToMenuBtnContent = "Вийти в меню";
            ViewModel.GoToWindowsBtnContent = "Вийти з гри";
            ViewModel.RestartText = "Перезапустити";
        }
    }
}
