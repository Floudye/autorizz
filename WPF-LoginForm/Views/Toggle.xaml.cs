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
    /// Логика взаимодействия для Toggle.xaml
    /// </summary>
    public partial class Toggle : UserControl
    {
        Thickness LeftSide = new Thickness(-39, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -39, 0);
        private bool Toggled = false;
        public Toggle()
        {
            InitializeComponent();
            Toggled = false;
            Dot.Margin = LeftSide;
        }
        public bool Toggled1 { get => Toggled; set => Toggled = value; }
        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {

                Toggled = false;
                Dot.Margin = LeftSide;

            }
        }

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {
                Toggled = false;
                Dot.Margin = LeftSide;

            }
        }
    }
}
