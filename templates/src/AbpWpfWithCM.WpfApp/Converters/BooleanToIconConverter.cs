using System;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 布尔值转图标转换器
    /// </summary>
    public class BooleanToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            string parameterString = parameter as string;
            
            if (string.IsNullOrEmpty(parameterString))
                return PackIconKind.QuestionMark;
                
            string[] options = parameterString.Split('|');
            string iconName = bValue ? options[0] : options[1];
            
            if (Enum.TryParse<PackIconKind>(iconName, out var iconKind))
                return iconKind;
                
            return PackIconKind.QuestionMark;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 