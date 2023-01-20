﻿using CourseWork.src.main.cs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork.src.main.cs.Views
{
    /// <summary>
    /// Логика взаимодействия для ChooseLevel.xaml
    /// </summary>
    public partial class ChooseLevel : Window
    {
        public ChooseLevel()
        {
            InitializeComponent();
            DataContext = new ChooseLevelViewModel(this);
        }
    }
}
