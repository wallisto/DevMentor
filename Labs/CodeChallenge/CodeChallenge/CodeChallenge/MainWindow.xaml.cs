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
        private int numberOfTimesHitBadRectangle;
        private int numberOfTimesMissedRectangle;
        public User player;

        bool? Easy;

        public MainWindow()
        {
            InitializeComponent();

           

            player = new User
            {
                Name = "Player1",
                Score = 0,
                // Can't pass in the image, not sure how to do it with URI?
               
               // BadImage = new Uri("asf")
            };

            DataContext = player;
            
            

        }

        Random r = new Random();
        private void GoodRectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //var uri = goodImage.UriSource;
            //goodImage.UriSource = new Uri("Images\\Michael_Kennedy.jpg");

            //goodImage.UriSource = new Uri(@"pack://application:,,,/XamlLab2;component/Images/Michael_Kennedy.jpg");

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

        private void EasyCommand(object sender, RoutedEventArgs e)
        {
            //Set background canvas
            Easy = null;

            BitmapImage img = new BitmapImage();
            //img.BeginInit();
            //img.UriSource = new Uri(@"pack://application:,,,/XamlLab2;component/Images/Michael_Kennedy.jpg");
            //img.EndInit();
            Backdrop.Source = img;

            HardButton.IsEnabled = true;
            EasyButton.IsEnabled = false;
            MediumButton.IsEnabled = true;

            goodRectangle.Height = 150;
            goodRectangle.Width = 150;
            badRectangle.Height = 150;
            badRectangle.Width = 150;
        }

        private void MediumCommand(object sender, RoutedEventArgs e)
        {
            //Set background canvas
            Easy = true;

            BitmapImage img = new BitmapImage();
            //img.BeginInit();
            //img.UriSource = new Uri(@"pack://application:,,,/XamlLab2;component/Images/Richard_Blewett.jpg");
            //img.EndInit();
            Backdrop.Source = img;

            HardButton.IsEnabled = true;
            EasyButton.IsEnabled = true;
            MediumButton.IsEnabled = false;
        }

        private void Hard(object sender, RoutedEventArgs e)
        {
            //Set background canvas
            Easy = false;

            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(@"pack://application:,,,/XamlLab2;component/Images/Crowd.jpg");
            img.EndInit();
            Backdrop.Source = img;

            HardButton.IsEnabled = false;
            EasyButton.IsEnabled = true;
            MediumButton.IsEnabled = true;
        }



    
        private void MissedRectangle(object sender, MouseButtonEventArgs e)
        {
            if (gameInProgress)
            {
                if (DateTime.Now < gameEnd)
                {
                    MoveRectangle();
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

        private void MoveRectangle()
        {
            ChooseWhichRectangleToShow();
            int randomX = r.Next(1400);
            int randomY = r.Next(600);
            Canvas.SetLeft(badRectangle, randomX);
            Canvas.SetTop(badRectangle, randomY);
            Canvas.SetLeft(goodRectangle, randomX);
            Canvas.SetTop(goodRectangle, randomY);
            if (Easy != null)
            {
                int randomSize = r.Next(20, 100);
                goodRectangle.Height = randomSize;
                goodRectangle.Width = randomSize;
                badRectangle.Height = randomSize;
                badRectangle.Width = randomSize;
            }
        }

       
        private void GameStarts(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Have you entered your name?", "Start New Game", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var win = new ChooseGoodBad(player, this);
                win.Show();
                
            }
                        
        }

        public void ActuallyStartGame()
        {
            //goodRectangle.Visibility = Visibility.Visible;
            //badRectangle.Visibility = Visibility.Visible;

            gameLength = new TimeSpan(100000000);
            gameInProgress = true;
            gameEnd = DateTime.Now + gameLength;
            numberOfTimesHitBadRectangle = 0;
            numberOfTimesMissedRectangle = 0;
            player.Score = 0;

            MoveRectangle();
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
            if (Easy == null)
            {
                File.AppendAllText(@"HighScores_Easy.txt", ((User)DataContext).Name + "\t" + ((User)DataContext).Score + Environment.NewLine);
           
            }
            else if(Easy.Value)
            {
                File.AppendAllText(@"HighScores_Medium.txt", ((User)DataContext).Name + "\t" + ((User)DataContext).Score + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(@"HighScores_Hard.txt", ((User)DataContext).Name + "\t" + ((User)DataContext).Score + Environment.NewLine);
            }
            
            var win = new EndGameWindow(player.Score, Easy);
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
        public string Name { get; set; }

        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; PropertyChanged(this, new PropertyChangedEventArgs("Score")); }
        }

        private Uri goodImage;
        public Uri GoodImage
        {
            get { return goodImage; }
            set { goodImage = value; PropertyChanged(this, new PropertyChangedEventArgs("GoodImage")); }
        }

        private Uri badImage;
        public Uri BadImage
        {
            get { return badImage; }
            set { badImage = value; PropertyChanged(this, new PropertyChangedEventArgs("BadImage")); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        public void Refresh()
        {
            PropertyChanged(this, null);
        }

       
    }
}
