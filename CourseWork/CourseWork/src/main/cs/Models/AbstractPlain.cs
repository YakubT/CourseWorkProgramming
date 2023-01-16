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
    public abstract class AbstractPlain:Enemy
    {
        

        protected HealthBar healthBar;

        public AbstractPlain():base()
        {

        }

        public AbstractPlain(FieldViewModel viewModel) : base(viewModel)
        {

        }

        protected override void finish(Grid grid, Image img)
        {
            grid.Children.Remove(img);
            grid.Children.Remove(healthBar.blackBar);
            grid.Children.Remove(healthBar.indicator);
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            viewModel.enemyList.Remove(this);
        }
        public override void ExecuteBodyMethod()
        {
            flyDirectionState[Convert.ToInt32(IsFromRight)].StartFlyPreprocessing(this);
            Image img = new Image();
            img.Source = sprite;
            img.Width = Width * viewModel.Window.ActualWidth / 24;
            img.Height = Height * viewModel.Window.ActualHeight / 24;
            img.Stretch = Stretch.Fill;
            Grid grid = (Grid)viewModel.Window.FindName("grid");
            CanvasUtility.addToGrid(img, grid);
            healthBar = new HealthBar();
            healthBar.blackBar = new Image();
            healthBar.blackBar.Source = new BitmapImage(new Uri("\\src\\main\\resources\\img\\black.png", UriKind.Relative));
            healthBar.blackBar.Stretch = Stretch.Fill;
            healthBar.indicator = new Rectangle();
            CanvasUtility.addToGrid(healthBar.blackBar, grid);
            CanvasUtility.addToGrid(healthBar.indicator, grid);
            healthBar.blackBar.Width = healthBar.indicator.Width = img.Width / 2;
            healthBar.blackBar.Height = healthBar.indicator.Height = 0.3 * viewModel.Window.ActualHeight / 24;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            DispatcherTimer timer = new DispatcherTimer();
            healthBar.indicator.Fill = new SolidColorBrush(Colors.Green);
            img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
            healthBar.indicator.Margin = healthBar.blackBar.Margin = new Thickness((coordinates.X + width / 4) * viewModel.Window.ActualWidth / 24.0, 0, 0, (coordinates.Y + 0.8) * viewModel.Window.ActualHeight / 24.0);
            img.RenderTransformOrigin = new Point(0, 0);
            timer.Interval = TimeSpan.FromMilliseconds(15);
            img.Visibility = Visibility.Visible;
            Image bah = new Image();
            img.RenderTransformOrigin = new Point(0, 0);
            viewModel.enemyList.Add(this);
            timer.Tick += async (sender, e) =>
            {
                coordinates.X += speed * timer.Interval.TotalSeconds;
                img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
                healthBar.indicator.Margin = healthBar.blackBar.Margin = new Thickness((coordinates.X + width / 4) * viewModel.Window.ActualWidth / 24.0, 0, 0, (coordinates.Y + 0.8) * viewModel.Window.ActualHeight / 24.0);
                healthBar.indicator.Width = Math.Max(health / maxHealth * healthBar.blackBar.Width, 0);

                if (health <= 0)
                {
                    timer.Stop();
                    end(bah, img, grid);
                }
                if (coordinates.X > 24 + 2 * width || coordinates.X < -2 * width)
                {
                    finish(grid, img);
                    timer.Stop();
                }
            };
            timer.Start();
        }
    }
}
