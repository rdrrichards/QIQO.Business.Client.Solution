using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QIQO.Custom.Controls
{
	public class BoldDateConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			// Exit if values not set
			if ((values[0] == null) || (values[1] == null)) return null;

			// Get values passed in
			var targetDate = (DateTime) values[0];
			var parent = (QIQOCalender) values[1];

			// Exit if highlighting turned off
			//if (parent.ShowDateHighlighting == false) return null;

			// Exit if no EventDates collection
			if (parent.EventDates == null) return null;

			/* The WPF calendar always displays six rows of dates, and it fills out those rows 
			 * with dates from the preceding and following month. These 'gray' date numbers (29,
			 * 30, 31, and so on, and 1, 2, 3, and so on) duplicate date numbers in the current 
			 * month, so we ignore them. The tool tips for these gray dates will appear in their 
			 * own display months. */

			// Exit if target date not in the current display month
			//if (!targetDate.IsSameMonthAs(parent.DisplayDate)) return null;

			// Get highlight text for date passed in
			bool boldDate = false;
			var day = targetDate.Day;

			/* The HighlightedDateText array is indexed from zero, while the calendar is indexed from
			 * one. So, we have to adjust the index between the array and the calendar. */

			// Get array index
			var n = day - 1;

			var edate = parent.EventDates.Where(d => d.Date == targetDate).FirstOrDefault();
			if (edate != null)
			{
				boldDate = true;
			}
			//var dateIsHighlighted = parent.EventDates.Contains(targetDate);
			//if (dateIsHighlighted) boldDate = true;

			// Set return value
			return boldDate;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return new object[0];
		}
	}
}
