using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceItemView.xaml
    /// </summary>
    public partial class InvoiceItemView : UserControl
    {
        public InvoiceItemView()
        {
            InitializeComponent();
            DataContext = new InvoiceItemViewModel();
        }
    }
}
