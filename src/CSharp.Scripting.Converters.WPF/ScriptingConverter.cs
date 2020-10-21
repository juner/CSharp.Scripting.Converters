using System;
using System.Globalization;
using System.Windows.Data;

namespace CSharp.Scripting.Converters.WPF
{
    public class ScriptingConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(string parameter, object?[] values)
            => Core.ScriptingConverter.Convert(parameter, values);
        object IMultiValueConverter.Convert(object?[] values, Type targetType, object parameter, CultureInfo culture)
            => Convert(parameter as string ?? throw new ArgumentException(nameof(parameter) + " is not string.", nameof(parameter)), values);
        object IValueConverter.Convert(object? value, Type targetType, object parameter, CultureInfo culture)
            => Convert(parameter as string ?? throw new ArgumentException(nameof(parameter) + " is not string.", nameof(parameter)), new object?[] { value });
        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
