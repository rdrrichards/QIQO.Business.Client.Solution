using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for OpenInvoiceView.xaml
    /// </summary>
    public partial class OpenInvoiceView : UserControl
    {
        public OpenInvoiceView(OpenInvoiceViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
