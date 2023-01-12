using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorPlain1:CreatorEmeny
    {
        public override Enemy Create()
        {
            return new Plain1();
        }
    }
}
