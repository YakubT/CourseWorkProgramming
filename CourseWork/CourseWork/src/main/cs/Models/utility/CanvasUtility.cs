using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.src.main.cs.Models.utility
{
    public static class CanvasUtility
    {
        public static void addToGrid(FrameworkElement o, Grid grid)
        {
            grid.Children.Add(o);
            Grid.SetRow(o, 0);
            Grid.SetColumn(o, 0);
            Grid.SetRowSpan(o, 24);
            Grid.SetColumnSpan(o, 24);
            Grid.SetZIndex(o, -1);
            o.HorizontalAlignment = HorizontalAlignment.Left;
            o.VerticalAlignment = VerticalAlignment.Bottom;
        }
    }
}
