using CourseWork.src.main.cs.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWork.src.main.cs.ViewModels
{
    public class UkrainianLabelClickCommand : ICommand
    {
        private MainWindowViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public UkrainianLabelClickCommand(MainWindowViewModel receiver)
        {
            this.receiver = receiver;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            PropertiesUtil properties = new PropertiesUtil(GlobalConstants.file);
            properties.setValue("language", "UA");
            receiver.UpdateLanguge();
        }
    }
}
