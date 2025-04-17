using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 布尔值转可见性转换器
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 将布尔值转换为可见性
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            bool reverse = parameter != null && parameter.ToString().ToLower() == "reverse";
            
            if (reverse)
                bValue = !bValue;
                
            return bValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 将可见性转换为布尔值
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            bool reverse = parameter != null && parameter.ToString().ToLower() == "reverse";
            bool result = visibility == Visibility.Visible;
            
            if (reverse)
                result = !result;
                
            return result;
        }
    }
}
