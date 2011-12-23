using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;


namespace Skyline.Silverlight.UI.Helpers
{
	/// <summary>
	///		Convert boolean to XAML Visibility
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value != null && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var visibility = (Visibility)value;
			return (visibility == Visibility.Visible);
		}
	}
}