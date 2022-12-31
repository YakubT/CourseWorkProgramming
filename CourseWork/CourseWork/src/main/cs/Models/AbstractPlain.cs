using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public abstract class AbstractPlain
    {
        private FlyDirectionState[] flyDirectionState;

        public BitmapImage sprite;

        protected FieldViewModel viewModel;

        protected Vector coordinates =new Vector();

        protected double height;

        protected double width;

        protected double speed;

        protected double health;

        public double Speed { get => speed; }

        public double Height { get => height; }

        public double Width { get => width; }

        public bool IsFromRight { get; set; }

        public Vector Coordinates { get => coordinates; }

        public AbstractPlain(FieldViewModel viewModel)
        {
            this.viewModel = viewModel;
            flyDirectionState = new FlyDirectionState[2];
            flyDirectionState[0] = new LeftFlyDirectionState();
            flyDirectionState[1] = new RightFlyDirectionState();
        }

        public void PlainPutLeft()
        {
            coordinates.X = -width;
        }

        public void PlainPutRight()
        {
            coordinates.X = 24 + width;
        }

        public void AntiSpeed()
        {
            speed *= -1;
        }

        protected abstract void SetSpeed();

        protected abstract void SetSize();
        public void Fly(Image img)
        {
            SetSpeed();
            SetSize();
            flyDirectionState[Convert.ToInt32(IsFromRight)].StartFlyPreprocessing(this);
            img.Source = sprite;
            img.Width = Width * viewModel.Window.ActualWidth / 24;
            img.Height = Height * viewModel.Window.ActualHeight / 24;
            
            DispatcherTimer timer = new DispatcherTimer();
            img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
            timer.Interval= TimeSpan.FromMilliseconds(15);
            img.Visibility = Visibility.Visible;
            timer.Tick += (sender, e) =>
            {

                coordinates.X += speed * timer.Interval.TotalSeconds;
                img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);

                if (coordinates.X>24+2*width|| coordinates.X < -2*width)
                {
                    Grid grid = (Grid)viewModel.Window.FindName("grid");
                    grid.Children.Remove(img);
                    timer.Stop();
                    GC.Collect(0);
                    GC.Collect(1);
                    GC.Collect(2);
                    GC.WaitForPendingFinalizers();
                    timer.Stop();
                }
            };
            timer.Start();
        }
    }
}
