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

namespace WPF_LoginForm.Views
{
    /// <summary>
    /// Логика взаимодействия для settings.xaml
    /// </summary>
    public partial class settings : Page
    {
        public settings()
        {
            InitializeComponent();
        }
        private void SwitchToggle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BtnToggle.Toggled1 == true)
            {
                ResourceDictionary dark = new ResourceDictionary();
                dark.Source = new Uri("Dark.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(dark);
            }
            else
            {
                ResourceDictionary Light = new ResourceDictionary();
                Light.Source = new Uri("Light.xaml", UriKind.Relative);
                System.Windows.Application.Current.Resources.MergedDictionaries.Add(Light);
            }
        }
    }
}
