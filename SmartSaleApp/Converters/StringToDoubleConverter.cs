using System.Globalization;

namespace SmartSaleApp.Converters;

public sealed class StringToDoubleConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return double.TryParse(value?.ToString(), out double result) ? result : null;
    }
}