using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderViewX : UserControl
    {
        //public OrderViewX() : this (new OrderViewModelX(ServiceLocator.Current.GetInstance<IEventAggregator>(),
        //    ServiceLocator.Current.GetInstance<IServiceFactory>(),
        //    ServiceLocator.Current.GetInstance<IProductListService>(),
        //    ServiceLocator.Current.GetInstance<IRegionManager>(),
        //    ServiceLocator.Current.GetInstance<IReportService>()))
        //{

        //}
        public OrderViewX(OrderViewModelX view_model) // 
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
