using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using CommonServiceLocator;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Common.Core.Logging;
using QIQO.Business.Module.Dashboard.Views;

namespace QIQO.Business.Module.Dashboard.ViewModels
{
    class DashboardNavigationViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public DashboardNavigationViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            ShowDashboardModuleCommand = new DelegateCommand(DoNavigate);
        }

        public DelegateCommand ShowDashboardModuleCommand { get; set; }

        private void DoSomething(string message)
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(message);
        }

        private void DoNavigate()
        {
            var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(DashboardRibbonView).FullName);
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(DashboardView).FullName, NavigationComplete, parameters);
            //_regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OpenOrderView);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(DashboardView).FullName);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            //Log.Debug(result.Context.NavigationService.ToString());
        }
    }
}
