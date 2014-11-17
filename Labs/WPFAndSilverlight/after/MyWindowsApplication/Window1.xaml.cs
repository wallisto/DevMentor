using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MyWindowsApplication
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        void OnRectangleMouseDown(object sender, MouseButtonEventArgs args)
        {
            Color c = new Color();
            Random r = new Random();

            c.A = (byte)r.Next(256);
            c.R = (byte)r.Next(256);
            c.G = (byte)r.Next(256);
            c.B = (byte)r.Next(256);
            myRectangle.Fill = new SolidColorBrush(c);
        }
    }
}