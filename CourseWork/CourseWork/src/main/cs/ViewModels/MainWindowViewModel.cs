using CourseWork.src.main.cs.Views;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using CourseWork.src.main.cs.utility;
using System.Collections.Generic;
using CourseWork.src.main.cs.ViewModels.intefaces;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string colorOfUkrLabel;

        private string colorOfEnLabel;

        private string buttonPlayText;

        private string buttonGuideText;

        private Dictionary<string, IMainViewModelLanguageState> dictionary = new Dictionary<string, IMainViewModelLanguageState>();

        public UkrainianLabelClick Label1Click { get; }

        public EnglishLabelClick Label2Click { get; }

        public GuideClick GuideClick { get; }

        public Window Window { get; }

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
            Label1Click = new UkrainianLabelClick(this);
            Label2Click = new EnglishLabelClick(this);
            GuideClick = new GuideClick(this);

        }

        public void UpdateLanguge()
        {
            string s = "";
            try
            {
                s = new PropertiesUtil(GlobalGonstants.file).getValue("language");
            }
            catch(Exception e)
            {
                s = "UA";
                new PropertiesUtil(GlobalGonstants.file).setValue("language", s);
            }
            dictionary[s].UpdateLanguage(this);
        }
    }

    public class EnglishLabelClick : ICommand
    {
        private MainWindowViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public EnglishLabelClick(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

     
        public void Execute(object parameter)
        {
            PropertiesUtil properties = new PropertiesUtil(GlobalGonstants.file);
            properties.setValue("language", "EN");
            receiver.UpdateLanguge();
        }
    }

   
    public class UkrainianLabelClick : ICommand
    {
        private MainWindowViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public UkrainianLabelClick(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            PropertiesUtil properties = new PropertiesUtil(GlobalGonstants.file);
            properties.setValue("language", "UA");
            receiver.UpdateLanguge();
        }
    }

    public class GuideClick : ICommand
    {
        private MainWindowViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public GuideClick (MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Guide guide = new Guide();
            guide.Show();
            receiver.Window.Close();
        }
    }
}
