using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceNavigationView.xaml
    /// </summary>
    public partial class InvoiceNavigationView : UserControl
    {
        public InvoiceNavigationView()
        {
            InitializeComponent();
            DataContext = new InvoiceNavigationViewModel();
        }
    }
}
