using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;

namespace PictureBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add(new CommandBinding(Commands.GetPictures, OnGetPictures));
        }

        Random rnd = new Random();

        private void OnGetPictures(object sender, ExecutedRoutedEventArgs e)
        {
            int pics = int.Parse(numPictures.Text);

            for (int i = 0; i < pics; i++)
            {
                WebRequest req = WebRequest.Create("http://localhost:9000/pictures/picture");

                WebResponse resp = req.GetResponse();
           
                PictureItem item = new PictureItem();

                BitmapImage bmp = new BitmapImage();

                bmp.DownloadCompleted += delegate
                {
                    resp.Close();
                };

                bmp.BeginInit();
                bmp.StreamSource = resp.GetResponseStream();
                bmp.EndInit();

                item.Source = bmp;
                
                double left = rnd.NextDouble() * board.ActualWidth;
                double top = rnd.NextDouble() * board.ActualHeight;
                Canvas.SetLeft(item, left);
                Canvas.SetTop(item, top);

                item.Angle = rnd.Next(-45, 45);

                board.Children.Add(item);
            }
        }
    }
}
