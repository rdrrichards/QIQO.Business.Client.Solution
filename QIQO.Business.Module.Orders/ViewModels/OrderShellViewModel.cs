//using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Orders.ViewModels
{
    public class OrderShellViewModel : ViewModelBase, IRegionMemberLifetime
    {
        public override string ViewTitle { get { return "Orders"; } }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public OrderShellViewModel(IRegionManager regionManager, IEventAggregator event_aggtr)
        {
            _regionManager = regionManager; // Unity.Container.Resolve<IRegionManager>(); ;
            event_aggregator = event_aggtr;

            NavigateCommand = new DelegateCommand<object>(Navigate);
            ApplicationCommands.OrdersNavigateCommand.RegisterCommand(NavigateCommand);
            //Navigate(typeof(OrderView));
            IsActive = true;
        }

        public bool KeepAlive { get; } = false;

        private void Navigate(object navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.OrdersRegion, navigatePath.ToString(), NavigationComplete);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug("From OrderShellViewModel " + result.Context.Uri.ToString());
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            event_aggregator.GetEvent<OrderUnloadingEvent>().Publish(navigationContext);
            ApplicationCommands.OrdersNavigateCommand.UnregisterCommand(NavigateCommand);
        }
    }
}
