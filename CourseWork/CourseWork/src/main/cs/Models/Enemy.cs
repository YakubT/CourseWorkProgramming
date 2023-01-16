﻿using CourseWork.src.main.cs.Models.utility;
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
    public abstract class Enemy
    {
        public double heightOfFly;

        protected FlyDirectionState[] flyDirectionState;

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

        public Vector Coordinates { get => coordinates; }

        public Enemy()
        {
            flyDirectionState = new FlyDirectionState[2];
            flyDirectionState[0] = new LeftFlyDirectionState();
            flyDirectionState[1] = new RightFlyDirectionState();

        }

        public Enemy(FieldViewModel viewModel) : this()
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

        }

        public abstract void ExecuteBodyMethod();
        public void Fly()
        {
            SetSpeed();
            SetDisplayProperty();
            coordinates.Y = heightOfFly;
            ExecuteBodyMethod();
            
        }
    }

  
}