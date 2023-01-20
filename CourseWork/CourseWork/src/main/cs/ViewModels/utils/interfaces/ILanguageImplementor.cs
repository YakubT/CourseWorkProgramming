using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.ViewModels.interfaces
{
    public interface ILanguageImplementor<T>
    {
        void UpdateLanguage(T ViewModel);
    }
}
