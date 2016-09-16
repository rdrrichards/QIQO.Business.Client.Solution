using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceShellView.xaml
    /// </summary>
    public partial class InvoiceShellView : UserControl
    {
        public InvoiceShellView(InvoiceShellViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
