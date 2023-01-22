using CourseWork.src.main.cs.Views;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using CourseWork.src.main.cs.utility;
using System.Collections.Generic;
using CourseWork.src.main.cs.ViewModels.intefaces;
using CourseWork.src.main.cs.ViewModels.utils;
using CourseWork.src.main.cs.ViewModels.utils.interfaces;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainWindowViewModel : BaseViewModel, ICloseableWindow
    {
        private string colorOfUkrLabel;

        private string colorOfEnLabel;

        private string buttonPlayText;

        private string buttonGuideText;

        private Dictionary<string, IMainViewModelLanguageState> dictionary = new Dictionary<string, IMainViewModelLanguageState>();

        public UkrainianLabelClickCommand Label1Click { get; }

        public EnglishLabelClickCommand Label2Click { get; }

        public GuideClickCommand GuideClickCommand { get; }

        public PlayClickCommand PlayClickCommand { get; }

        public Window Window { get; }

        public ShuttdownCommand ShutDown { get; }

        public string ColorOfUkrLabel
        {
            get => colorOfUkrLabel;
            set
            {
                colorOfUkrLabel = value;
                OnPropertyChanged(nameof(ColorOfUkrLabel));
            }
        }

        public string ColorOfEnLabel
        {
            get => colorOfEnLabel;
            set
            {
                colorOfEnLabel = value;
                OnPropertyChanged(nameof(ColorOfEnLabel));
            }
        }

        public string ButtonPlayText
        {
            get => buttonPlayText;
            set
            {
                buttonPlayText = value;
                OnPropertyChanged(nameof(ButtonPlayText));
            }
        }

        public string ButtonGuideText
        {
            get => buttonGuideText;
            set
            {
                buttonGuideText = value;
                OnPropertyChanged(nameof(ButtonGuideText));
            }
        }

        public MainWindowViewModel(Window window)
        {
            this.Window = window;
            dictionary["UA"] = new MainUkrainianLanguageImplementor();
            dictionary["EN"] = new MainEnglishLanguageImplementor();
            UpdateLanguge();
            Label1Click = new UkrainianLabelClickCommand(this);
            Label2Click = new EnglishLabelClickCommand(this);
            GuideClickCommand = new GuideClickCommand(this);
            PlayClickCommand = new PlayClickCommand(this);
            ShutDown = new ShuttdownCommand();
        }

        public void UpdateLanguge()
        {
            string s = "";
            try
            {
                s = new PropertiesUtil(GlobalConstants.file).getValue("language");
            }
            catch(Exception e)
            {
                s = "UA";
                new PropertiesUtil(GlobalConstants.file).setValue("language", s);
            }
            dictionary[s].UpdateLanguage(this);
        }
    }

   
}
