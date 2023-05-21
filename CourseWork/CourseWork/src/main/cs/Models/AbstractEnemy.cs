using CourseWork.src.main.cs.Models.utility;
using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
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
    public abstract class AbstractEnemy
    {
        protected DispatcherTimer timer = new DispatcherTimer();

        protected FlyDirectionStrategy[] flyDirectionState;

        public BitmapImage sprite;

        public BitmapImage sprite1;

        public BitmapImage spriteMirror;

        public FieldViewModel viewModel;

        public Vector coordinates = new Vector();

        protected double height;

        protected double width;

        protected double speed;

        public double maxHealth;

        public double health;

        public double Speed { get => speed; }

        public double Height { get => height; }

        public double Width { get => width; }

        public bool IsFromRight { get; set; }

        public double HeightOfFly { get; set; }

        public Vector Coordinates { get => coordinates; }

        public AbstractEnemy()
        {
            flyDirectionState = new FlyDirectionStrategy[2];
            flyDirectionState[0] = new LeftFlyDirectionStrategy();
            flyDirectionState[1] = new RightFlyDirectionStrategy();

        }

        public AbstractEnemy(FieldViewModel viewModel) : this()
        {
            this.viewModel = viewModel;
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


        protected abstract void finish(Grid grid, Image img);

        private void Pause(FieldViewModel fieldViewModel)
        {
            timer.Stop();
        }

        private void Resume(FieldViewModel fieldViewModel)
        {
            if (viewModel.enemyList.Contains(this))
             timer.Start();
        }

        protected abstract void reduceEnemy();
        protected void end(Image bah, Image img, Grid grid)
        {
            
            bah.Stretch = Stretch.Fill;
            bah.Source = new BitmapImage(new Uri("\\src\\main\\resources\\img\\fire.png", UriKind.Relative));
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
                timer2.Stop();
            };

            timer2.Start();
            reduceEnemy();
        }

        public abstract void ExecuteBodyMethod();
        public void Fly()
        {
            SetSpeed();
            SetDisplayProperty();
            coordinates.Y = HeightOfFly;
            viewModel.PauseEvent += Pause;
            viewModel.ResumeEvent += Resume;
            ExecuteBodyMethod();
            
        }
    }

  
}
