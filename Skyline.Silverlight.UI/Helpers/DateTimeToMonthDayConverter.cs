using System;
using System.Globalization;
using System.Windows.Data;

namespace Skyline.Silverlight.UI.Helpers
{
    public class DateTimeToMonthDayConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is DateTime)
            {
                DateTime date = (DateTime)value;

                return date.Month.ToString() + "/" + date.Day.ToString();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
