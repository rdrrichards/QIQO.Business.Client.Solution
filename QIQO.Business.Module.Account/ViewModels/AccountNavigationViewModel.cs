using System;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Account.Views;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountNavigationViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;
        private bool _nav_is_checked;

        public AccountNavigationViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();

            ShowAccountModuleCommand = new DelegateCommand(ShowAccountModule);
            event_aggregator.GetEvent<AccountLoadedEvent>().Subscribe(OnFormLoaded, ThreadOption.BackgroundThread);
        }

        private void OnFormLoaded(string obj)
        {
            IsNavButtonChecked = true;
        }
        public bool IsNavButtonChecked
        {
            get { return _nav_is_checked; }
            set { SetProperty(ref _nav_is_checked, value); }
        }

        public bool KeepAlive { get; } = false;

        private void ShowAccountModule()
        {
            var parameters = new NavigationParameters();
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, typeof(AccountRibbonView).FullName);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountView).FullName, NavigationComplete, parameters);
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            Log.Debug(result.Context.NavigationService.ToString());
        }

        public DelegateCommand ShowAccountModuleCommand { get; set; }
    }
}
