using CourseWork.src.main.cs.Models.utility;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public abstract class AbstractPlain
    {
        private HealthBar healthBar;

        private FlyDirectionState[] flyDirectionState;

        public BitmapImage sprite;

        public BitmapImage sprite1;

        public BitmapImage spriteMirror;

        protected FieldViewModel viewModel;

        public Vector coordinates =new Vector();

        protected double height;

        protected double width;

        protected double speed;

        public double maxHealth;

        public double health;

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

        protected abstract void SetDisplayProperty();


        private void finish(Grid grid, Image img)
        {
            grid.Children.Remove(img);
            grid.Children.Remove(healthBar.blackBar);
            grid.Children.Remove(healthBar.indicator);
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            viewModel.plainsList.Remove(this);
        }

        public void end(Image bah, Image img, Grid grid)
        {
            //MessageBox.Show("Hello");
            bah.Stretch = Stretch.Fill;
            bah.Source = new BitmapImage(new Uri("\\src\\main\\resources\\fire.png", UriKind.Relative));
            bah.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
            bah.Width = Width * viewModel.Window.ActualWidth / 24;
            bah.Height = 2 * Height * viewModel.Window.ActualHeight / 24;
            Grid.SetZIndex(bah, 1);
            CanvasUtility.addToGrid(bah, grid);
            bah.Visibility = Visibility.Visible;
            SoundPlayer soundPlayer = new SoundPlayer();
            soundPlayer.Stream = Properties.Resources.exploison;
            soundPlayer.Play();
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromMilliseconds(400);
            timer2.Tick += (o, s) => { 
                grid.Children.Remove(bah);
                finish(grid, img);
                timer2.Stop(); };

            timer2.Start();
          
        }
        public void Fly(Image img)
        {
            SetSpeed();
            SetDisplayProperty();
            flyDirectionState[Convert.ToInt32(IsFromRight)].StartFlyPreprocessing(this);
            img.Source = sprite;
            img.Width = Width * viewModel.Window.ActualWidth / 24;
            img.Height = Height * viewModel.Window.ActualHeight / 24;
            Grid grid = (Grid)viewModel.Window.FindName("grid");
            healthBar = new HealthBar();
            healthBar.blackBar = new Image();
            healthBar.blackBar.Source = new BitmapImage(new Uri("\\src\\main\\resources\\black.png", UriKind.Relative));
            healthBar.blackBar.Stretch = Stretch.Fill;
            healthBar.indicator = new Rectangle();
            CanvasUtility.addToGrid(healthBar.blackBar, grid);
            CanvasUtility.addToGrid(healthBar.indicator, grid);
            healthBar.blackBar.Width = healthBar.indicator.Width = img.Width/2;
            healthBar.blackBar.Height = healthBar.indicator.Height = 0.3 *viewModel.Window.ActualHeight / 24; ;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            DispatcherTimer timer = new DispatcherTimer();
            healthBar.indicator.Fill = new SolidColorBrush(Colors.Green);
            img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
            healthBar.indicator.Margin =  healthBar.blackBar.Margin = new Thickness((coordinates.X+width/4) * viewModel.Window.ActualWidth / 24.0, 0, 0, (coordinates.Y+0.8) * viewModel.Window.ActualHeight / 24.0);
            img.RenderTransformOrigin = new Point(0, 0);
            timer.Interval= TimeSpan.FromMilliseconds(15);
            img.Visibility = Visibility.Visible;
            Image bah = new Image();
            img.RenderTransformOrigin = new Point(0, 1);
            viewModel.plainsList.Add(this);
            timer.Tick += async (sender, e) =>
            {
                coordinates.X += speed * timer.Interval.TotalSeconds;
                img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
                healthBar.indicator.Margin =healthBar.blackBar.Margin = new Thickness((coordinates.X + width/4) * viewModel.Window.ActualWidth / 24.0, 0, 0, (coordinates.Y + 0.8) * viewModel.Window.ActualHeight / 24.0);
                healthBar.indicator.Width = Math.Max(health / maxHealth * healthBar.blackBar.Width,0);

                if (health <= 0)
                {
                    timer.Stop();
                    end(bah, img, grid);
                }
                if (coordinates.X>24+2*width|| coordinates.X < -2*width)
                { 
                    finish(grid,img);
                    timer.Stop();
                }
            };
            timer.Start();
        }
    }
}
