using CourseWork.src.main.cs.Models.utility;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public abstract class AbstractPatron
    {
        protected FieldViewModel fieldViewModel;

        protected double demage;

        protected double p;

        protected Vector coordinates;

        protected Vector speed;

        protected BitmapImage img;

        protected double height;

        protected double width;

        public Vector Coordinates { get => coordinates; }
        
        public Vector Speed { get => speed; }

        public double Height { get => height; }

        public double Width { get => width; }


        public double Demage { get => demage; set => demage = value; }

        protected void SetSpeedUsingAngleAndModule (double p, double angle )
        {
            speed = new Vector(p * Math.Cos(Math.PI/2-angle*Math.PI/180),p * Math.Sin(Math.PI / 2 - angle * Math.PI / 180));
        }

        protected abstract void SetStartSpeed(double angle);

        protected abstract void SetDisplayProperites();

        DispatcherTimer timer;

        Image img2;
        public void Abort(FieldViewModel link)
        {
            Grid grid = (Grid)link.Window.FindName("grid");
            grid.Children.Remove(img2);
            img = null;
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            link.patrons.Remove(this);
            timer.Stop();
        }
        public void StartFly(double angle, Image img, FieldViewModel link)
        {
            fieldViewModel = link;
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.Stream = Properties.Resources.boom1;
            soundPlayer.Play();
            SetStartSpeed(angle);
            SetDisplayProperites();
            img2 = img;
            img.Source = this.img;
             timer = new DispatcherTimer();
            speed.X *= (link.Window.ActualWidth / 24.0);
            speed.Y *= (link.Window.ActualWidth / 24.0);
            timer.Interval = TimeSpan.FromMilliseconds(20);
            double xGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunX"]);
            double yGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunY"]);
            double heightGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunHeight"]);
            double weightGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunWidth"]);
            coordinates = new Vector(link.Window.ActualWidth * (xGun-0.3) / 24.0 +(heightGun+weightGun) * link.Window.ActualHeight*Math.Sin(angle * Math.PI / 180) / 24.0, 
                link.Window.ActualHeight *yGun/ 24.0+ heightGun* link.Window.ActualHeight * Math.Cos(angle * Math.PI / 180) / 24.0);
            img.Margin = new Thickness(coordinates.X, 0, 0, coordinates.Y);
            img.RenderTransformOrigin = new Point(0.5, 0.5);
            img.Width = Width * link.Window.ActualWidth / 24;
            img.Height = Height * link.Window.ActualHeight / 24;
            img.Visibility = Visibility.Hidden;
            link.patrons.Add(this);
            timer.Tick += (object sender, EventArgs e) => 
            {
                speed.Y -= PhysicalConstants.g*(link.Window.ActualWidth / 24.0) * timer.Interval.Milliseconds/1000.0; 
                coordinates.X += speed.X*timer.Interval.Milliseconds / 1000.0; 
                coordinates.Y += speed.Y * timer.Interval.Milliseconds / 1000.0;
                img.Margin = new Thickness(coordinates.X, 0, 0, coordinates.Y);
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
                if (coordinates.X > link.Window.ActualWidth + img.Width || coordinates.Y > link.Window.ActualHeight + img.ActualHeight
                 || coordinates.X + img.Width < 0)
                {
                    Abort(link);

                }
                else
                {
                    img.Visibility = Visibility.Visible;
                }
                //MessageBox.Show(cooridinates.Y.ToString());
            };
            timer.Start();
        }
    }
}
