using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 相等性转可见性转换器
    /// </summary>
    public class EqualityToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;
                
            bool isEqual = value.Equals(parameter);
            return isEqual ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 