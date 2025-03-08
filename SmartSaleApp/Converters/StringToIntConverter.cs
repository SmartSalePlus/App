using System.Globalization;

namespace SmartSaleApp.Converters;

public sealed class StringToIntConverter : IValueConverter {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (int.TryParse(value?.ToString(), out int result)) {
            return result;
        }
        return null;
    }
}
