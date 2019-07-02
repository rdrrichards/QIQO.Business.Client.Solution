using QIQO.Business.Client.Entities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace QIQO.Business.Client.Core
{
    public class AttributeValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var att_type = (QIQOAttributeDataType)values[0];
            var att_value = (string)values[1];
            if (att_type == QIQOAttributeDataType.Number)
            {
                var num = 0;
                int.TryParse(att_value.ToString(), out num);
                return num.ToString("N0");
            }
            if (att_type == QIQOAttributeDataType.Money)
            {
                decimal num = 0;
                decimal.TryParse(att_value.ToString(), out num);
                return num.ToString("C2"); //string.Format(att_value, "0:C");
            }
            return att_value;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            decimal num = 0;
            decimal.TryParse(value.ToString(), out num);
            var objects = new object[1] { num };
            return objects;
        }
    }
}