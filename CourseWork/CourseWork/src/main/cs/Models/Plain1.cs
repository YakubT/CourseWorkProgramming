using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class Plain1 : AbstractPlain
    {
        public Plain1(FieldViewModel viewModel): base(viewModel) { this.coordinates.Y = 22; }
        protected override void SetSize()
        {
            width = 4;
            height = 1.5;
        }

        protected override void SetSpeed()
        {
            speed = 8;
        }
    }
}
