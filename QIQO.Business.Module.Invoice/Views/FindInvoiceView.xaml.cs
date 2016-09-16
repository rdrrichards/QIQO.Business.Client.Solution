using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for FindInvoiceView.xaml
    /// </summary>
    public partial class FindInvoiceView : UserControl
    {
        public FindInvoiceView()
        {
            InitializeComponent();
            DataContext = new FindInvoiceViewModel();
        }
    }
}
