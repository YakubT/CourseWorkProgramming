using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class CreatorPlain2:CreatorEmeny
    {
        public override AbstractEnemy Create()
        {
            return new Plain2();
        }
    }
}
