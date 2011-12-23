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

namespace Skyline.Silverlight.UI.Helpers
{
    public static class TextBoxExtensions
    {
        public static void SetText(this TextBox textBox, string text)
        {
            if (string.IsNullOrEmpty(text)) textBox.Text = "";
            else textBox.Text = text;
        }

        public static void SetText(this TextBox textBox, int? number)
        {
            if (number.HasValue) textBox.Text = number.Value.ToString();
            else textBox.Text = "";
        }

        public static string TextAsString(this TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text)) return null;
            else return textBox.Text;
        }

        public static Nullable<int> TextAsInteger(this TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text)) return null;
            else
            {
                int result = 0;
                if (int.TryParse(textBox.Text, out result)) return result;
                else
                {
                    textBox.Text = "";
                    return null;
                }
            }
        }

        public static Nullable<decimal> TextAsDecimal(this TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text)) return null;
            else
            {
                decimal result = 0;
                if (decimal.TryParse(textBox.Text, out result)) return result;
                else
                {
                    textBox.Text = "";
                    return null;
                }
            }
        }
    }
}
