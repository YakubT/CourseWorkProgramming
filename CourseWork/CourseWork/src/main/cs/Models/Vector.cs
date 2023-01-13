using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.src.main.cs.Models
{
    public class Vector
    {
        public double X { get; set;}

        public double Y { get; set; }

        public Vector() { }
        public Vector (double x, double y)
        {
            X = x;
            Y = y;
        }

        public static  Vector operator + (Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }
        public static Vector operator * (Vector a, double k)
        {
            return new Vector(k*a.X, k*a.Y);
        }
    }
}
