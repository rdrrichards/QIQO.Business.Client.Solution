using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceRibbonView.xaml
    /// </summary>
    public partial class InvoiceRibbonView : RibbonTab
    {
        public InvoiceRibbonView()
        {
            InitializeComponent();
            DataContext = new InvoiceRibbonViewModel();
        }
    }
}
