using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QIQO.Custom.Controls
{
    public class ToolTipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Exit if values not set
            if ((values[0] == null) || (values[1] == null)) return null;

            // Get values passed in
            var targetDate = (DateTime)values[0];
            var parent = (QIQOCalender)values[1];

            // Exit if no EventDates collection
            if (parent.EventDates == null) return null;

            // Get highlight text for date passed in
            string ttDate = null;

            IEnumerable<QIQODate> edate = parent.EventDates.Where(d => d.Date == targetDate).ToList();
            if (edate != null)
            {
                ttDate = "";
                foreach (QIQODate item in edate)
                {
                    ttDate = ttDate + $"Date: {item.Date.ToShortDateString()}\nDescription: {item.DateDescription}\nWho: {item.EntityName}\nWhat: {item.EntityType}\n\n";
                }
                //ttDate = $"Date: {edate.Date.ToShortDateString()}\nDescription: {edate.DateDescription}\nWho: {edate.EntityName}\nWhat: {edate.EntityType}";
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
