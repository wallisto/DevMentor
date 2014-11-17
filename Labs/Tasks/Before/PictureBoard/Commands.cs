using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PictureBoard
{
    public class Commands
    {
        public static RoutedCommand GetPictures = new RoutedCommand("GetPictures", typeof(Commands)); 
    }
}
