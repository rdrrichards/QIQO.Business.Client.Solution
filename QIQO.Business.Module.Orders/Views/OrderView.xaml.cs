using QIQO.Business.Client.Core;
using Microsoft.Practices.Unity;
using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;
using Prism.Events;
using QIQO.Business.Client.Core.UI;
using Prism.Regions;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView() : this (new OrderViewModel(Unity.Container.Resolve<IEventAggregator>(),
            Unity.Container.Resolve<IServiceFactory>(),
            Unity.Container.Resolve<IProductListService>(),
            Unity.Container.Resolve<IRegionManager>(),
            Unity.Container.Resolve<IReportService>()))
        {

        }
        public OrderView(OrderViewModel view_model) // 
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
