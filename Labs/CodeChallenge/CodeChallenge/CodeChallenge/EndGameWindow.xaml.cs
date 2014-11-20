using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {

        User player;
        bool? Easy;
        
        public EndGameWindow(int Score, bool? easy)
        {
            InitializeComponent();

            player = new User { Score = Score };
            Easy = easy;

            DataContext = player;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            

            var win = new HighScoresWindow(Easy);
            win.Show();
        }

        
    }


}
