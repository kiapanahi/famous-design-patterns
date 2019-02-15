using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.Converters
{
    
    // converts string to shorter string given a max length.

    [ValueConversion(typeof(string), typeof(string))]
    public sealed class MaxLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string length = parameter as string;
            if (string.IsNullOrEmpty(length))
                throw new ArgumentException();

            if (value == null)
                return Binding.DoNothing;

            string name = value.ToString();

            int maxLength;
            if (!int.TryParse(length, out maxLength))
                maxLength = name.Length;

            if (name.Length > maxLength)
            {
                // first try to break at last space

                int space = name.LastIndexOf(' ');
                if (space > 0)
                    name = name.Substring(0, space);
                else
                    name = name.Substring(0, maxLength) + "...";
            }
            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
