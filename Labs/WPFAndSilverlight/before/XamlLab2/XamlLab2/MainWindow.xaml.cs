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

namespace XamlLab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random r = new Random();
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Color c = RandomColor();
            myRectangle.Fill = new SolidColorBrush(c);

        }

        private Color RandomColor()
        {
            Color c = new Color();
            c.A = (byte)r.Next(256);
            c.R = (byte)r.Next(256);
            c.G = (byte)r.Next(256);
            c.B = (byte)r.Next(256);
            return c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Color c = RandomColor();
            myRectangle.Stroke = new SolidColorBrush(c);
        }
    }
}
