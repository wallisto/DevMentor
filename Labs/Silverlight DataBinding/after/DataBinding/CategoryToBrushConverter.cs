using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Data;

namespace DataBinding
{
  public class CategoryToBrushConverter : IValueConverter
  {
    public object Convert(object value, Type targetType,
          object parameter, CultureInfo culture)
    {
      ContactCategory category = (ContactCategory)value;
      Color c = Colors.Black;

      switch (category)
      {
        case ContactCategory.Friend: c = Colors.Green; break;
        case ContactCategory.Family: c = Colors.Blue; break;
        case ContactCategory.Colleague: c = Colors.Orange; break;
        case ContactCategory.Customer: c = Colors.Black; break;
        case ContactCategory.MedicalPractitioner:
          c = Colors.Red; break;
      }

      return new SolidColorBrush(c);
    }

    public object ConvertBack(object value, Type targetType,
          object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

}
