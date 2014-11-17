using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Data;

namespace WinPhilosophers.Converters
{
    public class EnumToBrushConverter : IValueConverter
    {
        public EnumToBrushConverter()
        {
            BrushMap = new Dictionary<string, Brush>();
        }
        public Type EnumType { get; set; }

        public Dictionary<string,Brush> BrushMap { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string strValue = Enum.GetName(EnumType, value);

            return BrushMap[strValue];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
