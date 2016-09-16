using QIQO.Business.Module.Invoices.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Invoices.Views
{
    /// <summary>
    /// Interaction logic for InvoiceView.xaml
    /// </summary>
    public partial class InvoiceViewX : UserControl
    {
        //public InvoiceViewX() : this (new InvoiceViewModel(Unity.Container.Resolve<IEventAggregator>(),
        //    Unity.Container.Resolve<IServiceFactory>(),
        //    Unity.Container.Resolve<IProductListService>(),
        //    Unity.Container.Resolve<IRegionManager>(),
        //    Unity.Container.Resolve<IReportService>()))
        //{

        //}
        public InvoiceViewX(InvoiceViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
