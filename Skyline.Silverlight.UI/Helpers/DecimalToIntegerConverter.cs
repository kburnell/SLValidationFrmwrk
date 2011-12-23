using System;
using System.Globalization;
using System.Windows.Data;

namespace Skyline.Silverlight.UI.Helpers
{
    public class DecimalToIntegerConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null && value is decimal)
            {
                Nullable<decimal> decimalValue = value as Nullable<decimal>;
                return decimal.ToInt32(decimalValue.Value);
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
