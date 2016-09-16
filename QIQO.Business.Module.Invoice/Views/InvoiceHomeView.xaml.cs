using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceHomeView.xaml
    /// </summary>
    public partial class InvoiceHomeView : UserControl
    {
        public InvoiceHomeView(InvoiceHomeViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
