using System;
using System.Globalization;
using System.Windows.Data;

namespace QIQO.Business.Client.Core
{
    public class FeeScheduleValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string fee_type = (string)values[0];
            decimal fee_value = (decimal)values[1];
            if (fee_type == "P")
            {
                return fee_value.ToString("P1");
            }
            else
            {
                return fee_value.ToString("C2");
            }
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            decimal num = 0;
            decimal.TryParse(value.ToString(), out num);
            object[] objects = new object[1]{num};
            return objects;
        }
    }
}