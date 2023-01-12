using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorDron : CreatorEmeny
    {
        public override Enemy Create()
        {
            return new Dron1();
        }
    }
}
