using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseWork.src.main.cs.Models
{
    public interface FlyDirectionState
    {
        void StartFlyPreprocessing(AbstractPlain plain);
    }
}
