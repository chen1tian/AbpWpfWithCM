using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 布尔值转画刷转换器
    /// </summary>
    public class BooleanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            string parameterString = parameter as string;
            
            if (string.IsNullOrEmpty(parameterString))
                return new SolidColorBrush(Colors.Black);
                
            string[] options = parameterString.Split('|');
            string colorName = bValue ? options[0] : options[1];

            try
            {
                var color = (Color)ColorConverter.ConvertFromString(colorName);
                return new SolidColorBrush(color);
            }
            catch
            {
                return new SolidColorBrush(Colors.Black);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 