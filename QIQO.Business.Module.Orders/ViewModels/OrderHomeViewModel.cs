using Prism.Commands;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Orders.Views;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderHomeViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public OrderHomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NewOrderCommand = new DelegateCommand(NewOrder);
        }
        public DelegateCommand NewOrderCommand { get; set; }

        private void NewOrder()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderViewX).FullName);
        }
    }
}
