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

        private TimeSpan gameLength;
        private DateTime gameStart;
        private DateTime gameEnd;


        private int _TotalScore = 10;

        public string TotalScore
        {
            get { return _TotalScore.ToString(); }
        }
        


        public MainWindow()
        {
            InitializeComponent();

            DataContext = new User
            {
                Name = "Player1",
                Score = TotalScore
            };
            

        }

        Random r = new Random();
        private void GoodRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            if (DateTime.Now < gameEnd)
            {
                UpdateScore(+1);
            }
            else
            {
                throw new Exception();
            }
        }

        private void BadRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            if (DateTime.Now < gameEnd)
            {
                UpdateScore(-1);
            }
            else
            {
                throw new Exception();
            }
        }
    
        private void MissedRectangle(object sender, MouseButtonEventArgs e)
        {
            if (DateTime.Now < gameEnd)
            {
                ChooseWhichRectangleToShow();
                int randomX = r.Next(375);
                int randomY = r.Next(200);
                Canvas.SetLeft(badRectangle, randomX);
                Canvas.SetTop(badRectangle, randomY);
                Canvas.SetLeft(goodRectangle, randomX);
                Canvas.SetTop(goodRectangle, randomY);
                int randomSize = r.Next(10, 30);
                goodRectangle.Height = randomSize;
                goodRectangle.Width = randomSize;
                badRectangle.Height = randomSize;
                badRectangle.Width = randomSize;
            }
            else
            {
                throw new Exception();
            }
        }


        private void GameStarts(object sender, RoutedEventArgs e)
        {
            gameLength = new TimeSpan(100000000);
            gameStart = DateTime.Now;
            gameEnd = gameStart + gameLength;
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

        private void UpdateScore(int increase)
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

    public class User
    {
        public string Name { get; set; }
        public string Score { get; set; }
    }
}
