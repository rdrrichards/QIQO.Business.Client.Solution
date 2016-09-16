using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for WorkingInvoiceView.xaml
    /// </summary>
    public partial class WorkingInvoiceView : UserControl
    {
        public WorkingInvoiceView(WorkingInvoiceViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
