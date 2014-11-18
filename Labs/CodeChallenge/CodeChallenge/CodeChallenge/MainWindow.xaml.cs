using System;
using System.Collections.Generic;
using System.IO;
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
        private bool gameInProgress;
        private DateTime gameEnd;
        private TextWriter writer;

        private int _TotalScore = 10;

        public string TotalScore
        {
            get { return Convert.ToString(_TotalScore); }
        }
        


        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists("..\\..\\HighScores.txt"))
            {
                writer = File.CreateText("..\\..\\HighScores.txt");
                writer.Close();
            }
                        
            DataContext = new User
            {
                Name = "Player1",
                Score = Convert.ToInt32(TotalScore)
            };
            
            

        }

        Random r = new Random();
        private void GoodRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            if (gameInProgress)
            {
                if (DateTime.Now < gameEnd)
                {
                    UpdateScore(+1);
                }
                else
                {
                    EndGame();
                }
            }
        }

        private void BadRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            if (gameInProgress)
            {
                if (DateTime.Now < gameEnd)
                {
                    UpdateScore(-1);
                }
                else
                {
                    EndGame();
                }
            }
        }
    
        private void MissedRectangle(object sender, MouseButtonEventArgs e)
        {
            if (gameInProgress)
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
                    EndGame();
                }
            }
        }


        private void GameStarts(object sender, RoutedEventArgs e)
        {
            gameLength = new TimeSpan(20000000);
            gameInProgress = true;
            gameEnd = DateTime.Now + gameLength;
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

        private void EndGame()
        {
            gameInProgress = false;
            File.AppendAllText(@"..\..\HighScores.txt", ((User)DataContext).Name + "\t" + ((User)DataContext).Score + Environment.NewLine);

            var top10players = Top10HighScores().Take<User>(10);
        }

        private List<User> Top10HighScores()
        {
            List<User> top10 = new List<User>();

            string line;
            using (StreamReader reader = new StreamReader("..\\..\\HighScores.txt"))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    top10.Add(new User
                    {
                        Name = line.Split('\t').First(),
                        Score = Convert.ToInt32(line.Split('\t').Last())
                    }
                        );
                }

            }

            List<User> orderedPlayers = top10.OrderByDescending(u => u.Score).ToList<User>();
            
            return orderedPlayers;
        }
       

    }

    public class User
    {
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
