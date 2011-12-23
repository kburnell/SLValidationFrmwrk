using System;
using System.Net;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
namespace Skyline.Silverlight.UI.Helpers
{
    public class DoubleToStringConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double parsedValue;

            if ((value != null) && (double.TryParse(value.ToString(), out parsedValue)))
            {
                if (parameter != null)
                {
                    return parsedValue.ToString(parameter.ToString(), culture);
                }
                else
                {
                    return parsedValue.ToString("N", culture);
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
