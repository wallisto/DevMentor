using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
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
        private int numberOfTimesHitBadRectangle;
        private int numberOfTimesMissedRectangle;
        private User player;
     
        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists("..\\..\\HighScores.txt"))
            {
                writer = File.CreateText("..\\..\\HighScores.txt");
                writer.Close();
            }

            player = new User
            {
                Name = "Player1",
                Score = 0,
            };

            DataContext = player;
            
            

        }

        Random r = new Random();
        private void GoodRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Color c = RandomColor();
            //goodRectangle.Fill = new SolidColorBrush(c);
            if (gameInProgress)
            {
                UpdateScore(+1);
                if (DateTime.Now < gameEnd)
                {
                    numberOfTimesMissedRectangle = 0;

                    //Add sound
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
                UpdateScore(-1);
                if (DateTime.Now < gameEnd)
                {
                    numberOfTimesHitBadRectangle++;
                    numberOfTimesMissedRectangle = 0;

                    //Add sound

                    if (numberOfTimesHitBadRectangle > 2)
                    {
                        EndGameWithAdmonishment();
                    }
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
                    int randomX = r.Next(1500);
                    int randomY = r.Next(1000);
                    Canvas.SetLeft(badRectangle, randomX);
                    Canvas.SetTop(badRectangle, randomY);
                    Canvas.SetLeft(goodRectangle, randomX);
                    Canvas.SetTop(goodRectangle, randomY);
                    int randomSize = r.Next(20, 100);
                    goodRectangle.Height = randomSize;
                    goodRectangle.Width = randomSize;
                    badRectangle.Height = randomSize;
                    badRectangle.Width = randomSize;

                    numberOfTimesMissedRectangle++;

                    //Add sound

                    if (numberOfTimesMissedRectangle > 3)
                    {
                        EndGameMissedRectangle();
                    }
                }
                else
                {
                    EndGame();
                }
            }
        }

       
        private void GameStarts(object sender, RoutedEventArgs e)
        {
            gameLength = new TimeSpan(100000000);
            gameInProgress = true;
            gameEnd = DateTime.Now + gameLength;
            numberOfTimesHitBadRectangle = 0;
            numberOfTimesMissedRectangle = 0;
            player.Score = 0;
        }

        private void UpdateScore(int increase)
        {
            player.Score += increase;
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

        private void EndGameMissedRectangle()
        {
            gameInProgress = false;

            var win = new ErrorWindow();
           
            win.Show();

            Stream str = Properties.Resources.Evil_Laugh;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
            
        }

        private void EndGameWithAdmonishment()
        {
            gameInProgress = false;

            var win = new RunOutOfLives();
            win.Show();

            Stream str = Properties.Resources.laugh_x;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
            
        }

        private void EndGame()
        {
            gameInProgress = false;
            File.AppendAllText(@"..\..\HighScores.txt", ((User)DataContext).Name + "\t" + ((User)DataContext).Score + Environment.NewLine);

            
            var win = new EndGameWindow(player.Score);
            win.Show();

            Stream str = Properties.Resources.Yahoo;
            SoundPlayer snd = new SoundPlayer(str);
            snd.Play();
            
        }

        


        List<string> pictures;

        public void PictureService()
        {
            DirectoryInfo dir = new DirectoryInfo(@"..\..\images");
            pictures.AddRange(dir.GetFiles().Select(fi => fi.FullName));
        }

        private Stream GetPhotosForRectangle()
        {
            string filename = pictures[r.Next(pictures.Count)];

            Console.WriteLine("Picture requested - returning {0}", filename);

            FileStream fs = File.OpenRead(filename);

            return fs;
        }

    }

    public class User : INotifyPropertyChanged
    {
        private int score;
        public string Name { get; set; }
        public int Score
        {
            get { return score; }
            set { score = value; PropertyChanged(this, new PropertyChangedEventArgs("Score")); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        public void Refresh()
        {
            PropertyChanged(this, null);
        }

       
    }
}
