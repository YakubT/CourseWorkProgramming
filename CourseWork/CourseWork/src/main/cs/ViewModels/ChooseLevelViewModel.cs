using CourseWork.src.main.cs.utility;
using CourseWork.src.main.cs.ViewModels.utils;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork.src.main.cs.ViewModels
{
    public class ChooseLevelViewModel : BaseViewModel, ICloseableWindow
    {
        private Dictionary<string, ILanguageChooseLevelState> dictionary = new Dictionary<string, ILanguageChooseLevelState>();

        private string titleText;

        private string backButtonContent;
        public Window Window { get; }

        public string BackButtonContent
        {
            get=>backButtonContent;
            set
            {
                backButtonContent = value;
                OnPropertyChanged(nameof(BackButtonContent));
            }
        }

        public string TitleText 
        { 
            get=>titleText;
            set
            {
                titleText = value;
                OnPropertyChanged(nameof(TitleText));
            }
        }

        public BackButtonClickCommand BackButtonClickCommand { get; }

        public ChooseLevelViewModel(Window window)
        {
            dictionary["EN"] = new ChooseLevelEnglishLanguageImplementor();
            dictionary["UA"] = new ChooseLevelUkrainianLanguageImplementor();
            string s = new PropertiesUtil(GlobalGonstants.file).getValue("language");
            dictionary[s].UpdateLanguage(this);
            BackButtonClickCommand = new BackButtonClickCommand(this);
            Window = window;
        }
    }
}
