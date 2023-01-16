using CourseWork.src.main.cs.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string colorOfUkrLabel;

        private string colorOfEnLabel;

        private string buttonPlayText;

        private string buttonGuideText;

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
            UpdateLanguge();
            Label1Click = new UkrainianLabelClick(this);
            Label2Click = new EnglishLabelClick(this);
            GuideClick = new GuideClick(this);
        }

        public void UpdateLanguge()
        {
            string s = ConfigurationManager.AppSettings["language"];
            if (s == "UA")
            {
                ColorOfUkrLabel = ColorTranslator.ToHtml(Color.GreenYellow);
                ColorOfEnLabel = ColorTranslator.ToHtml(Color.White);
                ButtonGuideText = "Інструкція";
                ButtonPlayText = "Грати";
            }
            else
            {
                ColorOfEnLabel = ColorTranslator.ToHtml(Color.GreenYellow);
                ColorOfUkrLabel = ColorTranslator.ToHtml(Color.White);
                ButtonGuideText = "Guide";
                ButtonPlayText = "Play";
            }
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
            ConfigurationManager.AppSettings["language"] = "EN";
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
            ConfigurationManager.AppSettings["language"] = "UA";
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
