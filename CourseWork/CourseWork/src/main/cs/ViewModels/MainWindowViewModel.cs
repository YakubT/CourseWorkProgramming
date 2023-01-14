using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWork.src.main.cs.ViewModels
{
    public class MainWindowViewModel: BaseViewModel
    {
        private string colorOfUkrLabel;

        private string colorOfEnLabel; 
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
                colorOfEnLabel= value;
                OnPropertyChanged(nameof(ColorOfEnLabel));
            }
        }

        public MainWindowViewModel()
        {
            string s = ConfigurationManager.AppSettings["language"];
            if (s == "UA")
            {
                ColorOfUkrLabel = ColorTranslator.ToHtml(Color.GreenYellow);
                ColorOfEnLabel = ColorTranslator.ToHtml(Color.White);
            }
            else
            {
                ColorOfEnLabel = ColorTranslator.ToHtml(Color.GreenYellow);
                ColorOfUkrLabel = ColorTranslator.ToHtml(Color.White);
            }
        }
    }
}
