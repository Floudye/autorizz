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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Логика взаимодействия для UserWindows.xaml
    /// </summary>
    public partial class UserWindows : Window
    {
        public UserWindows()
        {
            InitializeComponent();
        }

        private void bd_click(object sender, MouseButtonEventArgs e)
        {
            MainFrane.Content = new GridUser();
        }

        private void setting_click(object sender, MouseButtonEventArgs e)
        {
            MainFrane.Content = new settings();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
