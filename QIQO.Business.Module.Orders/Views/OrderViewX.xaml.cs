using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderViewX : UserControl
    {
        //public OrderViewX() : this (new OrderViewModelX(Unity.Container.Resolve<IEventAggregator>(),
        //    Unity.Container.Resolve<IServiceFactory>(),
        //    Unity.Container.Resolve<IProductListService>(),
        //    Unity.Container.Resolve<IRegionManager>(),
        //    Unity.Container.Resolve<IReportService>()))
        //{

        //}
        public OrderViewX(OrderViewModelX view_model) // 
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
