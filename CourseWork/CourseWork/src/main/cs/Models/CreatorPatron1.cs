using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorPatron1 : CreatorPatron
    {
        public override AbstractPatron Create()
        {
            return new Patron1();
        }
    }
}
