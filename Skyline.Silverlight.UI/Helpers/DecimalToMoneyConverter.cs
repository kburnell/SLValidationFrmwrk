using System;
using System.Globalization;
using System.Windows.Data;

namespace Skyline.Silverlight.UI.Helpers
{
	public class DecimalToMoneyConverter : IValueConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter">Unused parameter.  This uses Resource CurrencyFormat</param>
		/// <param name="culture"></param>
		/// <returns>Converted object or original value</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			decimal parsedValue;

			if ((value != null) && (decimal.TryParse(value.ToString(), out parsedValue)))
			{
				return parsedValue.ToString("F2", culture);
			}

			return value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter">Unused parameter.</param>
		/// <param name="culture"></param>
		/// <returns>Converted object or null</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = value as string;

			if(stringValue == null)
				return value;

			stringValue = stringValue.Replace(culture.NumberFormat.CurrencySymbol, "");

			decimal parsedValue;
			if (decimal.TryParse(stringValue, out parsedValue))
			{
				return parsedValue;
			}

			return null;
		}

	}
}