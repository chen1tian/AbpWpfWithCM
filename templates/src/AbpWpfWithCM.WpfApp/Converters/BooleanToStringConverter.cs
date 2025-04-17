using System;
using System.Globalization;
using System.Windows.Data;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 布尔值转字符串转换器
    /// </summary>
    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            string parameterString = parameter as string;
            
            if (string.IsNullOrEmpty(parameterString))
                return bValue.ToString();
                
            string[] options = parameterString.Split('|');
            return bValue ? options[0] : options[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 