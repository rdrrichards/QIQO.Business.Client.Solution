using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace QIQO.Business.Client.Core
{
    public class MouseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value)
                    return Cursors.Wait;
                else
                    return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
