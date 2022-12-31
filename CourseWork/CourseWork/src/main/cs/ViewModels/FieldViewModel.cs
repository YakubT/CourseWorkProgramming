﻿using CourseWork.src.main.cs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            double tg = ((((MouseEventArgs)parameter).GetPosition(receiver.Window).Y -
                receiver.Window.ActualHeight * 21.5 / 24) /
                (((MouseEventArgs)parameter).GetPosition(receiver.Window).X - receiver.Window.ActualWidth* 16.45/ 24.0));
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
            Patron patron = creators[receiver.WheelType].Create();
            Image img = new Image();
            img.Stretch = System.Windows.Media.Stretch.Fill;
            img.Visibility = Visibility.Visible;
            Grid grid = (Grid)receiver.Window.FindName("grid");
            grid.Children.Add(img);
            Grid.SetRow(img,0);
            Grid.SetColumn(img,0);
            Grid.SetRowSpan(img,24);
            Grid.SetColumnSpan(img,24);
            Grid.SetZIndex(img, -1);
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Bottom;
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
            if (((MouseWheelEventArgs)parameter).Delta > 0)
            {
                receiver.WheelType = (receiver.WheelType + 1) % 2;
            }
            else
            {
                receiver.WheelType = (receiver.WheelType -1+2) % 2;
            }
        }
    }
    public class FieldViewModel : BaseViewModel
    {
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
        public FieldViewModel(Window window)
        {
            this.window = window;
            RotateGunCommand = new GunRotateCommand(this);
            PatronStartFly = new PatronStartFly(this);
            WheelScroll = new WheelScroll(this);
        }

        public Window Window { get => window; }

       
    }
}
