﻿using System;
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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Логика взаимодействия для BDGrid.xaml
    /// </summary>
    public partial class BDGrid : Page
    {
        public BDGrid()
        {
            InitializeComponent();
        }

        private void edit_click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new EditUsers();
        }
    }
}
