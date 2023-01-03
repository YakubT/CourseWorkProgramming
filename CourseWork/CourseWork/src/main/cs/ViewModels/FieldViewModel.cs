﻿using CourseWork.src.main.cs.Models;
using CourseWork.src.main.cs.Models.utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork.src.main.cs.ViewModels
{
    public class GunRotateCommand : ICommand
    {
        private FieldViewModel receiver;
        
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public GunRotateCommand (FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            double xGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunX"]);
            double yGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunY"]);
            double heightGun = Convert.ToDouble(ConfigurationManager.AppSettings["GunHeight"]);
            double tg = ((((MouseEventArgs)parameter).GetPosition(receiver.Window).Y -
                receiver.Window.ActualHeight * (24-yGun) / 24) /
                (((MouseEventArgs)parameter).GetPosition(receiver.Window).X - receiver.Window.ActualWidth*xGun / 24.0));
            double angle = 180*Math.Atan(Math.Abs(tg))/Math.PI;
            if (tg > 0)
            {
                angle = -90 + angle;
            }
            else
                angle = 90 - angle;
            if (angle>-60 && angle<50)
            this.receiver.Angle = angle;
           
        }
    }

    public class PatronStartFly : ICommand
    {
        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public PatronStartFly(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            CreatorPatron[] creators = new CreatorPatron[2];
            creators[0] = new CreatorPatron1();
            creators[1] = new CreatorPatron2();
            AbstractPatron patron = creators[receiver.WheelType].Create();
            Image img = new Image();
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Visibility = Visibility.Visible;
            Grid grid = (Grid)receiver.Window.FindName("grid");
            FrameworkElement o = (FrameworkElement)img;
            CanvasUtility.addToGrid(o, grid);
            patron.StartFly(receiver.Angle, img, receiver);

            

        }
    }

    public class WheelScroll : ICommand
    {
        private FieldViewModel receiver;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public WheelScroll(FieldViewModel receiver)
        {
            this.receiver = receiver;
        }
        public void Execute(object parameter)
        {
            try
            {
                if (((MouseWheelEventArgs)parameter).Delta > 0)
                {
                    receiver.WheelType = (receiver.WheelType + 1) % 2;
                }
                else
                {
                    receiver.WheelType = (receiver.WheelType - 1 + 2) % 2;
                }
            }
            catch(InvalidCastException e)
            {
                receiver.WheelType = (receiver.WheelType + 1) % 2;
            }

            
        }
    }
    public class FieldViewModel : BaseViewModel
    {
        public List<AbstractPlain> plainsList = new List<AbstractPlain>();

        public List<AbstractPatron> patrons = new List<AbstractPatron>();

        private Window window;
        public GunRotateCommand RotateGunCommand { get;}

        public PatronStartFly PatronStartFly { get; }

        public WheelScroll WheelScroll { get; }
        
        private double angle;

        private uint wheelType = 0; 

        public double Angle
        {
            get => angle;
            set
            {
                angle = value;
                OnPropertyChanged(nameof(Angle));
            }
        }

        public uint WheelType
        {
            get => wheelType;
            set => wheelType = value;
        }

        DispatcherTimer dispatcherTimer;
        public FieldViewModel(Window window)
        {
            this.window = window;
            RotateGunCommand = new GunRotateCommand(this);
            PatronStartFly = new PatronStartFly(this);
            WheelScroll = new WheelScroll(this);

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(4);

            dispatcherTimer.Tick += (s, e) =>
             {
                 AbstractPlain plain = new Plain1(this);
                 Image img2 = new Image();
                 img2.Stretch = System.Windows.Media.Stretch.Fill;
                 img2.Visibility = Visibility.Visible;
                 Grid grid2 = (Grid)this.Window.FindName("grid");

                 grid2.Children.Add(img2);
                 Grid.SetRow(img2, 0);
                 Grid.SetColumn(img2, 0);
                 Grid.SetRowSpan(img2, 24);
                 Grid.SetColumnSpan(img2, 24);
                 Grid.SetZIndex(img2, -1);
                 img2.HorizontalAlignment = HorizontalAlignment.Left;
                 img2.VerticalAlignment = VerticalAlignment.Bottom;
                 Random rnd = new Random();
                 plain.IsFromRight = Convert.ToBoolean(rnd.Next(2));
                 plain.Fly(img2);
             };
            dispatcherTimer.Start();
            DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
            dispatcherTimer2.Interval = TimeSpan.FromMilliseconds(2);
            dispatcherTimer2.Tick += (s, e) =>
              {
                  for (int i = 0; i<plainsList.Count;i++)
                  {
                      for (int j = 0; j < patrons.Count; j++)
                      {
                          if (check(plainsList[i],patrons[j]))
                          {
                              plainsList[i].health -= patrons[j].Demage;
                              patrons[j].Abort(this);
                          }
                      }
                  }
              };
            dispatcherTimer2.Start();
        }
        
        public double Soriented(Models.Vector a,Models.Vector b, Models.Vector c)
        {
            return (a.X - b.X) * (b.Y + a.Y) / 2.0+(c.X - a.X) * (c.Y + a.Y) / 2.0+ (b.X - c.X) * (b.Y + c.Y) / 2.0;
        }
        public bool check(AbstractPlain abstractPlain, AbstractPatron abstractPatron)
        {
            double cos = abstractPatron.Speed.X / Math.Sqrt(abstractPatron.Speed.X * abstractPatron.Speed.X + abstractPatron.Speed.Y * abstractPatron.Speed.Y);
            double sin = abstractPatron.Speed.Y / Math.Sqrt(abstractPatron.Speed.X * abstractPatron.Speed.X + abstractPatron.Speed.Y * abstractPatron.Speed.Y);

            Models.Vector v1, v2,v3,v4,v5,v6;
            v1 = new Models.Vector();
            v2 = new Models.Vector();
            v3 = new Models.Vector();
            v4 = new Models.Vector();
            v5 = new Models.Vector();
            v6 = new Models.Vector();
            v1.X = abstractPatron.Coordinates.X + abstractPatron.Height* window.ActualHeight/24.0 / 2.0 * cos;
            v1.Y = abstractPatron.Coordinates.Y + abstractPatron.Height * window.ActualHeight/24.0 / 2.0 * sin;
            v2.X = abstractPatron.Coordinates.X - abstractPatron.Height * window.ActualHeight/24.0 / 2.0 * cos;
            v2.Y = abstractPatron.Coordinates.Y - abstractPatron.Height * window.ActualHeight/24.0 / 2.0 * sin;
            v3.X = abstractPlain.Coordinates.X * window.ActualWidth/24.0;
            v3.Y = abstractPlain.Coordinates.Y * window.ActualHeight/24.0;
            v4.X = (abstractPlain.Coordinates.X+abstractPlain.Width) * window.ActualWidth/24.0;
            v4.Y = abstractPlain.Coordinates.Y * window.ActualHeight/24.0;
            v5.X = (abstractPlain.Coordinates.X + abstractPlain.Width) * window.ActualWidth/24.0;
            v5.Y = (abstractPlain.Coordinates.Y+abstractPlain.Height) * window.ActualHeight/24.0;
            v6.X = abstractPlain.Coordinates.X * window.ActualWidth/24.0;
            v6.Y = (abstractPlain.Coordinates.Y + abstractPlain.Height) * window.ActualHeight/24.0;
            if ((Soriented(v1, v3, v4)) * (Soriented(v2, v3, v4))<=0 && (Soriented(v3, v1, v2)) * (Soriented(v4, v1, v2)) <= 0)
            {
                return true;
            }
            if ((Soriented(v1, v6, v5)) * (Soriented(v2, v6, v5)) <= 0 && (Soriented(v6, v1, v2)) * (Soriented(v5, v1, v2))<=0)
            {
                
                return true;
            }
            if ((Soriented(v1, v3, v6)) * (Soriented(v2, v3, v6)) <= 0 && (Soriented(v3, v1, v2)) * (Soriented(v6, v1, v2)) <= 0)
            {
                
                return true;
            }
            if ((Soriented(v1, v4, v5)) * (Soriented(v2, v4, v5)) <= 0 && (Soriented(v4, v1, v2)) * (Soriented(v5, v1, v2)) <= 0)
            {
                return true;
            }
            return false;
        }

        public Window Window { get => window; }

       
    }
}
