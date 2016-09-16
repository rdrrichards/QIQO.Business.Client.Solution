using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for OpenInvoiceView.xaml
    /// </summary>
    public partial class OpenInvoiceViewX : UserControl
    {
        public OpenInvoiceViewX(OpenInvoiceViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
