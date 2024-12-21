using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace isdf_bmeditor.ViewModels;

public class BoolToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && parameter is string strParameter)
        {
            var parts = strParameter.Split('|');
            return boolValue ? parts[0] : parts[1];
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 