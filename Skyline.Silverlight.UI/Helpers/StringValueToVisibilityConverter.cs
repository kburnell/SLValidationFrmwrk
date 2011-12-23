using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace Skyline.Silverlight.UI.Helpers
{
    public class StringValueToVisibilityConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else if (value is string)
            {
                if (string.IsNullOrEmpty((string)value)) return Visibility.Collapsed;
                else return Visibility.Visible;
            }
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
