using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Common.Core.Logging;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceShellViewModel : ViewModelBase, IRegionMemberLifetime
    {
        public override string ViewTitle { get { return "Invoices"; } }

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public InvoiceShellViewModel(IRegionManager regionManager, IEventAggregator event_aggtr)
        {
            _regionManager = regionManager; // ServiceLocator.Current.GetInstance<IRegionManager>(); ;
            event_aggregator = event_aggtr;

            NavigateCommand = new DelegateCommand<object>(Navigate);
            ApplicationCommands.InvoicesNavigateCommand.RegisterCommand(NavigateCommand);
            //Navigate(typeof(InvoiceView));
            IsActive = true;
        }

        public bool KeepAlive { get; } = false;

        private void Navigate(object navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate(RegionNames.InvoicesRegion, navigatePath.ToString(), NavigationComplete);
            }
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug("From InvoiceShellViewModel " + result.Context.Uri.ToString());
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            event_aggregator.GetEvent<InvoiceUnloadingEvent>().Publish(navigationContext);
            ApplicationCommands.InvoicesNavigateCommand.UnregisterCommand(NavigateCommand);
        }
    }
}
