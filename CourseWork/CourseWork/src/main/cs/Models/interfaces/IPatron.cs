using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseWork.src.main.cs.Models.interfaces
{
    public interface IPatron
    {
        void StartFly(double angle, Image img, FieldViewModel link);
    }
}
