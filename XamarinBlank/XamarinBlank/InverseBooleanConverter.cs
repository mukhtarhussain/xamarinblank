using System;
using System.Globalization;
using Xamarin.Forms;

namespace XamarinBlank.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool?)value ?? true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
         => !(value as bool?);
    }
}
