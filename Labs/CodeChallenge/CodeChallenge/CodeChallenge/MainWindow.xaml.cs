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
        private int _TotalScore { get; set; }

        public MainWindow()
        {
            InitializeComponent();


        }

        Random r = new Random();
        private void GoodRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            TotalScore(+1);

        }

        private void BadRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            TotalScore(-1);

        }
    
        private void MissedRectangle(object sender, MouseButtonEventArgs e)
        {
            ChooseWhichRectangleToShow();
            Canvas.SetLeft(badRectangle, r.Next(375));
            Canvas.SetTop(badRectangle, r.Next(200));
            Canvas.SetLeft(goodRectangle, r.Next(375));
            Canvas.SetTop(goodRectangle, r.Next(200));
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
            goodRectangle.Stroke = new SolidColorBrush(c);
        }

        private void TotalScore(int increase)
        {
            _TotalScore += increase;
        }

        private void ChooseWhichRectangleToShow()
        {
            int random = r.Next(10);

            if (random < 2)
            {
                goodRectangle.Visibility = System.Windows.Visibility.Collapsed;
                badRectangle.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                badRectangle.Visibility = System.Windows.Visibility.Collapsed;
                goodRectangle.Visibility = System.Windows.Visibility.Visible;
            }
        }

    }
}
