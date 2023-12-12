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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private readonly Brush darkThemeBackground = new SolidColorBrush(Color.FromRgb(33, 33, 33));
        private readonly Brush darkThemeForeground = Brushes.White;

        private readonly Brush lightThemeBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private readonly Brush lightThemeForeground = Brushes.Black;

        private void УстановитьТёмнуюТему()
        {
            // Установка фона и цвета переднего плана для тёмной темы
            this.Background = darkThemeBackground;
            ПрименитьТемуКЭлементам(this, darkThemeForeground);
        }

        private void УстановитьСветлуюТему()
        {
            // Установка фона и цвета переднего плана для светлой темы
            this.Background = lightThemeBackground;
            ПрименитьТемуКЭлементам(this, lightThemeForeground);
        }

        private void ПрименитьТемуКЭлементам(DependencyObject element, Brush foreground)
        {
            // Рекурсивно применить тему ко всем дочерним элементам
            if (element is Control control)
            {
                control.Foreground = foreground;
            }

            int childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(element, i);
                ПрименитьТемуКЭлементам(child, foreground);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }

        private void ToggleTheme()
        {
            if (toggleDarkTheme.IsChecked == true)
            {
                Resources["LightThemeStyle"] = null;
                Resources["DarkThemeStyle"] = FindResource("DarkThemeStyle");
            }
            else
            {
                Resources["LightThemeStyle"] = FindResource("LightThemeStyle");
                Resources["DarkThemeStyle"] = null;
            }
        }
        private void toggleDarkTheme_Checked(object sender, RoutedEventArgs e)
        {
            ToggleTheme();
        }
        private void ToggleDarkTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleTheme();
        }
    }
}
