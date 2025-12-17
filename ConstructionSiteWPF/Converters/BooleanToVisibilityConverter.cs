using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ConstructionSiteWPF.Converters
{
    /// <summary>
    /// Конвертер для преобразования bool в Visibility
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Если передан параметр "inverse", инвертируем
                bool isInverse = parameter != null && parameter.ToString().ToLower() == "inverse";

                if (isInverse)
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }

                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Visible;
            }

            return false;
        }
    }
}