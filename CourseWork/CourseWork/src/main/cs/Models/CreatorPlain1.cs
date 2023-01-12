using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorPlain1:CreatorPlain
    {
        public override AbstractPlain Create()
        {
            return new Plain1();
        }
    }
}
