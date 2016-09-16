using System;
using System.Windows;
using System.Windows.Media;

namespace QIQO.Custom.Controls
{
    public class QIQODate
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public QIQODateType DateType { get; set; } = QIQODateType.OrderDeliverByDate;
        public string DateDescription { get; set; }
        public string EntityType { get; set; } = "Account";
        public string EntityName { get; set; } = "Unknown";
        public Brush BackgroundBrush { get; set; } = Brushes.LightGreen;
        public Brush ForegroundBrush { get; set; } = Brushes.Black;
        public FontWeight FontWeight { get; set; } = FontWeights.Bold;
        public FontStyle FontStyle { get; set; } = FontStyles.Normal;

    }

    public enum QIQODateType
    {
        AccountStartDate,
        AccountAniversaryDate,
        AccountEndDate,
        BirthDate,
        FeeStartDate,
        FeeEndFee,
        SaleStartDate,
        SaleEndDate,
        OrderDeliverByDate,
        OrderBeginDate,
        OrderBakeCompleteDate,
        InvoiceDueDate,
        Holiday
    }
}
