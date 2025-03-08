using System.Globalization;

namespace SmartSaleApp.Converters;

public sealed class StringToDoubleConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (double.TryParse(value?.ToString(), out double result)) {
            return result;
        }
        return null;
    }
}