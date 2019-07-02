using CommonServiceLocator;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView() : this(new OrderViewModel(ServiceLocator.Current.GetInstance<IEventAggregator>(),
            ServiceLocator.Current.GetInstance<IServiceFactory>(),
            ServiceLocator.Current.GetInstance<IProductListService>(),
            ServiceLocator.Current.GetInstance<IRegionManager>(),
            ServiceLocator.Current.GetInstance<IReportService>()))
        {

        }
        public OrderView(OrderViewModel view_model) // 
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
