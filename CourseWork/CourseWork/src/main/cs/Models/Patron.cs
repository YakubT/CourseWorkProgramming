using CourseWork.src.main.cs.Models.utility;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public abstract class Patron
    {

        static protected double p;

        protected Vector cooridinates;

        protected Vector speed;

        protected BitmapImage img;

        protected double height;

        protected double width;

        public Vector Coordinates { get => cooridinates; }
        
        public Vector Speed { get => speed; }

        public double Height { get => height; }

        public double Width { get => width; }

        protected void SetSpeedUsingAngleAndModule (double p, double angle )
        {
            speed = new Vector(p * Math.Cos(Math.PI/2-angle*Math.PI/180),p * Math.Sin(Math.PI / 2 - angle * Math.PI / 180));
        }

        public abstract void SetStartSpeed(double angle);

        public abstract void SetDisplayProperites();
        public void StartFly(double angle, Image img, FieldViewModel link)
        {
            SetStartSpeed(angle);
            SetDisplayProperites();
         
            img.Source = this.img;
            DispatcherTimer timer = new DispatcherTimer();
            speed.X *= (link.Window.ActualWidth / 24.0);
            speed.Y *= (link.Window.ActualWidth / 24.0);
            timer.Interval = TimeSpan.FromMilliseconds(20);
            cooridinates = new Vector(link.Window.ActualWidth * 16 / 24.0 + link.Window.ActualWidth * 5 *Math.Sin(angle * Math.PI / 180) / 24.0, 
                link.Window.Height * 1.5/ 24.0 + link.Window.ActualWidth * 5 * Math.Cos(angle * Math.PI / 180) / 24.0);
            img.Margin = new Thickness(cooridinates.X, 0, 0, cooridinates.Y);
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            img.Width = Width * link.Window.ActualWidth / 24;
            img.Height = Height * link.Window.ActualHeight / 24;
            img.Visibility = Visibility.Hidden;
            timer.Tick += (object sender, EventArgs e) => 
            {
                speed.Y -= PhysicalConstants.g*(link.Window.ActualWidth / 24.0) * timer.Interval.Milliseconds/1000.0; 
                cooridinates.X += speed.X*timer.Interval.Milliseconds / 1000.0; 
                cooridinates.Y += speed.Y * timer.Interval.Milliseconds / 1000.0;
                img.Margin = new Thickness(cooridinates.X, 0, 0, cooridinates.Y);
                double cos = (speed.X/ (Math.Sqrt(speed.X * speed.X + speed.Y * speed.Y)));
                double sin = (speed.Y / (Math.Sqrt(speed.X * speed.X + speed.Y * speed.Y)));
                double angle2 = 0;
                if (cos >= 0 && sin >= 0)
                    angle2 = 90 - Math.Acos(cos) * 180 / Math.PI;
                else
                    if (cos >= 0 && sin < 0)
                    angle2 = 90 + Math.Acos(cos) * 180 / Math.PI;
                else
                    if (cos < 0 && sin >= 0)
                    angle2 = Math.Asin(sin) * 180 / Math.PI - 90;
                else
                    angle2 = -90 + Math.Asin(sin) * 180 / Math.PI;
                img.RenderTransform = new RotateTransform(angle2);
                img.Width = Width * link.Window.ActualWidth / 24;
                img.Height = Height * link.Window.ActualHeight / 24;
                if (cooridinates.X > link.Window.ActualWidth + img.Width || cooridinates.Y > link.Window.ActualHeight + img.ActualHeight
                 || cooridinates.X+img.Width<0)
                {
                    
                    Grid grid = (Grid)link.Window.FindName("grid");
                    grid.Children.Remove(img);
                    img = null;
                    GC.Collect(0);
                    GC.Collect(1);
                    GC.Collect(2);
                    GC.WaitForPendingFinalizers();
                    timer.Stop();
                }
                else
                img.Visibility = Visibility.Visible;
                //MessageBox.Show(cooridinates.Y.ToString());
            };
            timer.Start();
        }
    }
}
