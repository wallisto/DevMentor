using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fundamentals
{
	public class TextBoxExtender
	{
		private static void OnIsRequiredChanged(DependencyObject dobj, DependencyPropertyChangedEventArgs e)
		{
			TextBox target = dobj as TextBox;

			if (target == null)
				return;

			bool connecting = (bool)e.NewValue;

			if (connecting)
			{
				target.TextChanged += TargetTextChanged;
			}
			else
			{
				target.TextChanged -= TargetTextChanged;
			}

			TargetTextChanged(target, null);
		}

		private static void TargetTextChanged(object sender, TextChangedEventArgs e)
		{
			TextBox target = sender as TextBox;

			target.Background = (target.Text.Length == 0) ?	errorBrush : normalBrush;
		}

		private static SolidColorBrush normalBrush = new SolidColorBrush(Colors.White);
		private static SolidColorBrush errorBrush  = new SolidColorBrush(Color.FromArgb(255, 255, 189, 189));

		public static bool GetIsRequired(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsRequiredProperty);
		}

		public static void SetIsRequired(DependencyObject obj, bool value)
		{
			obj.SetValue(IsRequiredProperty, value);
		}

		public static readonly DependencyProperty IsRequiredProperty =
			DependencyProperty.RegisterAttached("IsRequired",
			typeof(bool),
			typeof(TextBoxExtender),
			new PropertyMetadata(false, OnIsRequiredChanged));
	}
}