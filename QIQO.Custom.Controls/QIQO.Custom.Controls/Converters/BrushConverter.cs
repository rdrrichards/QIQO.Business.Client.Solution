using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace QIQO.Custom.Controls
{
    public class BackgroundBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Exit if values not set
            if ((values[0] == null) || (values[1] == null))
            {
                return null;
            }

            // Get values passed in
            var targetDate = (DateTime)values[0];
            var parent = (QIQOCalender)values[1];

            // Exit if no EventDates collection
            if (parent.EventDates == null)
            {
                return null;
            }

            // Get highlight text for date passed in
            Brush ttDate = null;

            var edate = parent.EventDates.Where(d => d.Date == targetDate).FirstOrDefault();
            if (edate != null)
            {
                ttDate = edate.BackgroundBrush;
            }

            // Set return value
            return ttDate;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[0];
        }
    }

    public class ForegroundBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Exit if values not set
            if ((values[0] == null) || (values[1] == null))
            {
                return null;
            }

            // Get values passed in
            var targetDate = (DateTime)values[0];
            var parent = (QIQOCalender)values[1];

            // Exit if no EventDates collection
            if (parent.EventDates == null)
            {
                return null;
            }

            // Get highlight text for date passed in
            Brush ttDate = null;

            var edate = parent.EventDates.Where(d => d.Date == targetDate).FirstOrDefault();
            if (edate != null)
            {
                ttDate = edate.ForegroundBrush;
            }

            // Set return value
            return ttDate;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[0];
        }
    }
}
