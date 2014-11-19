﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace XamlLab2
{
    /// <summary>
    /// Interaction logic for HighScoresWindow.xaml
    /// </summary>
    public partial class HighScoresWindow : Window
    {
        HighScores top10scores;
        public HighScoresWindow()
        {
           InitializeComponent();

           var top10players = Top10HighScores().Take<User>(10);

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

        public List<User> Top10HighScores()
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

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();          
        }

        private void Button_Click_Reset(object sender, RoutedEventArgs e)
        {
            //Need a confirmation request to reset highscores
            if (true)
            {
                ClearHighScores();
            }
        }

        private void ClearHighScores()
        {
            File.CreateText("..\\..\\HighScores.txt");
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