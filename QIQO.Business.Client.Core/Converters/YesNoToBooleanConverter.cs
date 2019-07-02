using System;
using System.Globalization;
using System.Windows.Data;

namespace QIQO.Business.Client.Core
{
    public class YesNoToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var yn = (string)value;
            if (yn.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var yn = (bool)value;
            if (yn)
            {
                return "Y";
            }
            else
            {
                return "";
            }
        }
    }
}
