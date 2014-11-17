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
using System.IO;

namespace PictureBoard
{
    /// <summary>
    /// Interaction logic for PictureItem.xaml
    /// </summary>
    public partial class PictureItem : UserControl
    {
        public double Angle
        {
            get { return rotator.Angle; }
            set { rotator.Angle = value; }
        }

        public ImageSource Source
        {
            get { return img.Source; }
            set { img.Source = value; }
        }

        public PictureItem()
        {
            InitializeComponent();
        }
    }
}
