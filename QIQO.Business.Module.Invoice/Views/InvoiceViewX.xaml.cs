using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceViewX : UserControl
    {
        //public InvoiceViewX() : this (new InvoiceViewModel(ServiceLocator.Current.GetInstance<IEventAggregator>(),
        //    ServiceLocator.Current.GetInstance<IServiceFactory>(),
        //    ServiceLocator.Current.GetInstance<IProductListService>(),
        //    ServiceLocator.Current.GetInstance<IRegionManager>(),
        //    ServiceLocator.Current.GetInstance<IReportService>()))
        //{

        //}
        public InvoiceViewX(InvoiceViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
