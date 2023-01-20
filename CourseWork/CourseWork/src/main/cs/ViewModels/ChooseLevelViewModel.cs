using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels
{
    public class ChooseLevelViewModel:BaseViewModel
    {
        private string titleText;
        public string TitleText 
        { 
            get=>titleText;
            set
            {
                titleText = value;
                OnPropertyChanged(nameof(TitleText));
            }
        }

        public ChooseLevelViewModel()
        {
            
        }
    }
}
