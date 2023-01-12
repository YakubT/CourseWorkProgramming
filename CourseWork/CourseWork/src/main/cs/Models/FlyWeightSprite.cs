using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CourseWork.src.main.cs.Models
{
    public class FlyWeightSprite
    {
        private BitmapImage bitmap;

        public BitmapImage GetBitmap { get => bitmap; }

        public FlyWeightSprite(BitmapImage bitmap)
        {
            this.bitmap = bitmap;
        }
    }
}
