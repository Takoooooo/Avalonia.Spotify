using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace AvaloniaApplication14.Converters
{
    public class SecondsToTimeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var seconds = TimeSpan.FromSeconds(System.Convert.ToInt32(value));

            return $"{seconds:mm\\:ss}";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
