using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for FindInvoiceView.xaml
    /// </summary>
    public partial class FindInvoiceViewX : UserControl
    {
        public FindInvoiceViewX(FindInvoiceViewModelX viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
