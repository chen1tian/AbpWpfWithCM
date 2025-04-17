using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace AbpWpfWithCM.WpfApp.Converters
{
    /// <summary>
    /// 相等性转换器
    /// </summary>
    public class MultiEqualityConverter : IMultiValueConverter
    {
        public MultiEqualityConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 如果values为空，返回false
            if (values == null || values.Length == 0)
            {
                return false;
            }

            // 如果values中的对象都相等，返回true
            return values.Distinct().Count() == 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 