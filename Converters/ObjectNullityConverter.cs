using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Cephalog.Converters
{
    public class ObjectNullityConverter : IValueConverter
    {
        public Object NullValue { get; set; } = Visibility.Collapsed;
        public Object NotNullValue { get; set; } = Visibility.Visible;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is null)
            {
                return NullValue;
            }
            else
            {
                return NotNullValue;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
