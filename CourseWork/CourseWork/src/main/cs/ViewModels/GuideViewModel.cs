using CourseWork.src.main.cs.ViewModels.interfaces;
using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CourseWork.src.main.cs.utility;
using CourseWork.src.main.cs.ViewModels.utils;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;

namespace CourseWork.src.main.cs.ViewModels
{
    public class GuideViewModel : BaseViewModel, ICloseableWindow
    {
        private string backButtonContent;

        private Window window;

        private Dictionary<string,IGuideChangeLanguageStrategy> dictionary= new Dictionary<string,IGuideChangeLanguageStrategy>(); 

        public Window Window { get=>window; }

        public ICommand BackButtonClick{get;}
        public string BackButtonContent
        {
            get => backButtonContent;
            set
            {
                backButtonContent = value;
                OnPropertyChanged(backButtonContent);
            }
        }

        public GuideViewModel(Window window)
        {
            this.window = window;
            dictionary["UA"] = new UkrainianLanguageGuide();
            dictionary["EN"] = new EnglishLanguageGuide();
            dictionary[new PropertiesUtil(GlobalConstants.file).getValue("language")].UpdateLanguage(this);
            BackButtonClick = new BackButtonClickCommand(this);
        }
    }

}
