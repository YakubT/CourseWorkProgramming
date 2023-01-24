using CourseWork.src.main.cs.Models.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.Models
{
    public abstract class AbstractDron : AbstractEnemy
    {
        protected override void finish(Grid grid, Image img)
        {
            grid.Children.Remove(img);
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect(2);
            GC.WaitForPendingFinalizers();
            viewModel.enemyList.Remove(this);
        }
        public override void ExecuteBodyMethod()
        {
            Image img = new Image();
            flyDirectionState[Convert.ToInt32(IsFromRight)].StartFlyPreprocessing(this);
            img.Source = sprite;
            img.Width = Width * viewModel.Window.ActualWidth / 24;
            img.Height = Height * viewModel.Window.ActualHeight / 24;
            Grid grid = (Grid)viewModel.Window.FindName("grid");
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
            img.Stretch = Stretch.Fill;
            img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);
            img.RenderTransformOrigin = new Point(0, 0);
            CanvasUtility.addToGrid(img, grid);
            timer.Interval = TimeSpan.FromMilliseconds(15);
            img.Visibility = Visibility.Visible;
            Image bah = new Image();
            img.RenderTransformOrigin = new Point(0, 0);
            viewModel.enemyList.Add(this);
            double k = 2.5;
            double mid = coordinates.Y - k; 
            timer.Tick += async (sender, e) =>
            {
                coordinates.X += speed*timer.Interval.TotalSeconds;
                coordinates.Y = k * Math.Sin(coordinates.X)+mid;
                img.Margin = new Thickness(coordinates.X * viewModel.Window.ActualWidth / 24.0, 0, 0, coordinates.Y * viewModel.Window.ActualHeight / 24.0);

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
