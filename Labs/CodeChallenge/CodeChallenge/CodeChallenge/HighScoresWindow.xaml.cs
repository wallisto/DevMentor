using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for HighScoresWindow.xaml
    /// </summary>
    public partial class HighScoresWindow : Window
    {
        HighScores top10scores;
        bool? Easy;
        public HighScoresWindow(bool? easy)
        {

           InitializeComponent();

           Easy = easy;

           var top10players = Top10HighScores(Easy).Take<User>(10);

            string top10Names = null;
           string top10Scores = null;

           foreach (var player in top10players)
           {
               top10Names += player.Name + '\n';
               top10Scores += player.Score.ToString() + '\n';
           }

           top10Names = top10Names.Remove((top10Names.Length - 1));
           top10Scores = top10Scores.Remove((top10Scores.Length - 1));

           top10scores = new HighScores
           {
               Players = top10Names,
               Scores = top10Scores
           };

           DataContext = top10scores;
        }

        public List<User> Top10HighScores(bool? easy)
        {
            List<User> top10 = new List<User>();
            string[] file;
            if (easy == null)
            {
               file = File.ReadAllLines("HighScores_Easy.txt");
            }
            else if(easy.Value)
            {
               file = File.ReadAllLines("HighScores_Medium.txt");
            }
            else
            {
                file = File.ReadAllLines("HighScores_Hard.txt");
            }
                foreach(var line in file)
                {
                    top10.Add(new User
                    {
                        Name = line.Split('\t').First(),
                        Score = Convert.ToInt32(line.Split('\t').Last())
                    }
                        );
                }

            List<User> orderedPlayers = top10.OrderByDescending(u => u.Score).ToList<User>();

            return orderedPlayers;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();          
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            //Need a confirmation request to reset highscores

            MessageBoxResult dialogResult = MessageBox.Show("Do you REALLY want to delete the High Scores?", "Delete?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                ClearHighScores();
                this.Close();
            }
        }

        private void ClearHighScores()
        {
            if (Easy == null)
            {
                File.CreateText("HighScores_Easy.txt");
            }
            else if (Easy.Value)
            {
                File.CreateText("HighScores_Medium.txt");
            }
            else
            {
                File.CreateText("HighScores_Hard.txt");
            }
        }
    }

    public class HighScores : INotifyPropertyChanged
    {
        private string highScores;
        public string Scores
        {
            get { return highScores; }
            set { highScores = value; PropertyChanged(this, new PropertyChangedEventArgs("Scores")); }
        }

        private string highScoresNames;
        public string Players
        {
            get { return highScoresNames; }
            set { highScoresNames = value; PropertyChanged(this, new PropertyChangedEventArgs("Players")); }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void Refresh()
        {
            PropertyChanged(this, null);
        }

    }
}
