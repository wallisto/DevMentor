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

namespace XamlLab2
{
    /// <summary>
    /// Interaction logic for ChooseGoodBad.xaml
    /// </summary>
    public partial class ChooseGoodBad : Window
    {
        private User player;

        private MainWindow mainWindow;
        public ChooseGoodBad(User player, MainWindow mainWindow)
        {
            InitializeComponent();
            this.player = player;
            this.mainWindow = mainWindow;
        }

        private void Andrew_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Andrew_Clymer.jpg"));

            mainWindow.goodRectangle.Fill = new ImageBrush(image);

            Andrew.StrokeThickness = 2;
            Andrew.Stroke = Brushes.Green;

            Rich.StrokeThickness = 0;
            Mike.StrokeThickness = 0;

            Andrew_Bad.Visibility = Visibility.Collapsed;
            Mike_Bad.Visibility = Visibility.Visible;
            Rich_Bad.Visibility = Visibility.Visible;

            if (Andrew_Bad.StrokeThickness == 0)
            {
                GoButton.IsEnabled = true;
            }
            else
            {
                GoButton.IsEnabled = false;
            }
        }

        private void Rich_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Richard_Blewett.jpg"));

            mainWindow.goodRectangle.Fill = new ImageBrush(image);

            Rich.StrokeThickness = 2;
            Rich.Stroke = Brushes.Green;

            Andrew.StrokeThickness = 0;
            Mike.StrokeThickness = 0;

            Andrew_Bad.Visibility = Visibility.Visible;
            Mike_Bad.Visibility = Visibility.Visible;
            Rich_Bad.Visibility = Visibility.Collapsed;

            if (Rich_Bad.StrokeThickness == 0)
            {
                GoButton.IsEnabled = true;
            }
            else
            {
                GoButton.IsEnabled = false;
            }
        }

        private void Mike_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Michael_Kennedy.jpg"));

            mainWindow.goodRectangle.Fill = new ImageBrush(image);

            Mike.StrokeThickness = 2;
            Mike.Stroke = Brushes.Green;

            Andrew.StrokeThickness = 0;
            Rich.StrokeThickness = 0;

            Andrew_Bad.Visibility = Visibility.Visible;
            Mike_Bad.Visibility = Visibility.Collapsed;
            Rich_Bad.Visibility = Visibility.Visible;

            if (Mike_Bad.StrokeThickness == 0)
            {
                GoButton.IsEnabled = true;
            }
            else
            {
                GoButton.IsEnabled = false;
            }
        }

        private void Andrew_Bad_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Andrew_Clymer.jpg"));

            mainWindow.badRectangle.Fill = new ImageBrush(image);

            Andrew_Bad.StrokeThickness = 2;
            Andrew_Bad.Stroke = Brushes.Red;

            Mike_Bad.StrokeThickness = 0;
            Rich_Bad.StrokeThickness = 0;

            GoButton.IsEnabled = true;
        }

        private void Rich_Bad_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Richard_Blewett.jpg"));

            mainWindow.badRectangle.Fill = new ImageBrush(image);

            Rich_Bad.StrokeThickness = 2;
            Rich_Bad.Stroke = Brushes.Red;

            Mike_Bad.StrokeThickness = 0;
            Andrew_Bad.StrokeThickness = 0;

            GoButton.IsEnabled = true;
        }

        private void Mike_Bad_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var image = new BitmapImage(new Uri(@"pack://application:,,,/XamlLab2;component/Images/Michael_Kennedy.jpg"));

            mainWindow.badRectangle.Fill = new ImageBrush(image);

            Mike_Bad.StrokeThickness = 2;
            Mike_Bad.Stroke = Brushes.Red;

            Rich_Bad.StrokeThickness = 0;
            Andrew_Bad.StrokeThickness = 0;

            GoButton.IsEnabled = true;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            mainWindow.player = player;

            this.Close();

            mainWindow.ActuallyStartGame();
        }


    }
}
