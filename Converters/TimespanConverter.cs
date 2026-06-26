using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Cephalog.BusinessLogic;

namespace Cephalog.Converters
{
    public class TimespanConverter : IValueConverter
    {
        public string? Format { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                return ts.ToString(Format);
            }
            else
            {
                return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimespanCeilQuarterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                var ceiledQuarters = BusinessService.Instance.CeilToMinuteDivision(ts, 15);
                var timespanToDisplay = TimeSpan.FromMinutes(ceiledQuarters * 15);
                var totalHours = (int)timespanToDisplay.TotalHours;
                var totalDays = totalHours / 7;
                totalHours %= 7;
                if (totalDays > 0)
                {
                    return $"{totalDays:0}d {totalHours:00}:{timespanToDisplay.Minutes:00} ({timespanToDisplay.TotalMinutes / (7 * 60):0.0}jh)";
                }
                else
                {
                    return $"{totalHours:00}:{timespanToDisplay.Minutes:00} ({timespanToDisplay.TotalMinutes / (7 * 60):0.0}jh)";
                }
            }
            else
            {
                return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
