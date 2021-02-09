﻿using E3Tech.VideoRecorder.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E3Tech.VideoRecorder.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class E3VideoRecorder : UserControl
    {
        public E3VideoRecorder(E3VideoRecorderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}