using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Dashboard.ViewModels
{
    public class DashboardViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public DashboardViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            SomeCommand = new DelegateCommand<string>(DoSomething);
            OpenOrderCommand = new DelegateCommand<object>(OpenOrder);
        }

        public bool KeepAlive { get; } = true;

        public DelegateCommand<string> SomeCommand { get; set; }
        public DelegateCommand<object> OpenOrderCommand { get; set; }

        private void DoSomething(string message)
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(message);
        }

        private void OpenOrder(object order_key)
        {
            var parameters = new NavigationParameters();
            parameters.Add("order_key", order_key);
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, "QIQO.Business.Module.Order.Views.OrderRibbonView");
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "QIQO.Business.Module.Order.Views.OrderView", NavigationComplete, parameters);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            Log.Debug(result.Context.NavigationService.ToString());
        }
    }
}
