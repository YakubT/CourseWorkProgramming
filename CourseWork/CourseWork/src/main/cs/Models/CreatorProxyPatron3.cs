using CourseWork.src.main.cs.Models.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorProxyPatron3 : CreatorProxyPatron
    {
        public override IPatron Create()
        {
            return new ProxyPatron3();
        }
    }
}
