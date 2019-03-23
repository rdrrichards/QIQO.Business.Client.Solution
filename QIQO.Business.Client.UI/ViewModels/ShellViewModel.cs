using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Common.Core.Logging;
using Prism.Interactivity.InteractionRequest;

namespace QIQO.Business.Client.UI
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public DelegateCommand<object> NavigateCommand { get; private set; }
        public InteractionRequest<IConfirmation> GeneralSettingsRequest { get; set; }
        public InteractionRequest<IConfirmation> UserSettingsRequest { get; set; }
        public InteractionRequest<IConfirmation> PrintSetupRequest { get; set; }

        public ShellViewModel(IRegionManager regionManager, IEventAggregator event_aggtr)
        {
            _regionManager = regionManager;
            event_aggregator = event_aggtr;

            NavigateCommand = new DelegateCommand<object>(Navigate);
            ApplicationCommands.NavigateCommand.RegisterCommand(NavigateCommand);
            //event_aggregator.GetEvent<AccountNewOrderEvent>().Subscribe(AccountNewOrder);
            SomeCommand = new DelegateCommand<string>(DoSomething);
            ShowUserPreferencesCommand = new DelegateCommand(ShowUserPreferences);
            ShowGeneralPreferencesCommand = new DelegateCommand(ShowGeneralPreferences);
            ShowPrintSetupDialogCommand = new DelegateCommand(ShowPrintSetupDialog);
            GeneralSettingsRequest = new InteractionRequest<IConfirmation>();
            UserSettingsRequest = new InteractionRequest<IConfirmation>();
            PrintSetupRequest = new InteractionRequest<IConfirmation>();
        }

        private void ShowUserPreferences()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.UserSettingsDialogTitle;
            UserSettingsRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        // do something!
                    }
                }); //)
        }

        private void ShowGeneralPreferences()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.ApplicationSettingsDialogTitle;
            GeneralSettingsRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        // do something!
                    }
                }); //)
        }

        private void ShowPrintSetupDialog()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.PrintSetupDialogTitle;
            PrintSetupRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        // do something!
                    }
                }); //)
        }

        public DelegateCommand<string> SomeCommand { get; set; }
        public DelegateCommand ShowUserPreferencesCommand { get; set; }
        public DelegateCommand ShowGeneralPreferencesCommand { get; set; }
        public DelegateCommand ShowPrintSetupDialogCommand { get; set; }

        private void DoSomething(string message)
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(message);
        }

        //private void AccountNewOrder(string obj)
        //{
        //    //Unity.Container.Registrations.
        //    //var view = ServiceLocator.Current.GetInstance<OrderView>();
        //    var parameters = new NavigationParameters();
        //    parameters.Add("AccountCode", obj);
        //    //_regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderView).FullName, NavigationComplete, parameters);
        //    //_regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderView).FullName);
        //    IRegion cnt_region = _regionManager.Regions[RegionNames.ContentRegion];
        //    OrderView ord_view = ServiceLocator.Current.GetInstance<OrderView>();
        //    cnt_region.Add(ord_view);
        //    cnt_region.Activate(ord_view);
        //    //_regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OrderView).FullName, parameters);
        //    //Navigate(obj);
        //}

        private void Navigate(object navigatePath)
        {
            if (navigatePath != null)
            {
                string navPath = navigatePath.ToString();

                if (navPath.Contains("Account"))
                {
                    _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.AccountRibbonView);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, navPath, NavigationComplete);
                }

                if (navPath.Contains("Order"))
                {
                    _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.OrderRibbonView);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderShellView, NavigationComplete);
                    _regionManager.RequestNavigate(RegionNames.OrdersRegion, navPath, NavigationComplete);
                }

                if (navPath.Contains("Invoice"))
                {
                    _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.InvoiceRibbonView);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.InvoiceShellView, NavigationComplete);
                    _regionManager.RequestNavigate(RegionNames.InvoicesRegion, navPath, NavigationComplete);
                }

                if (navPath.Contains("Company"))
                {
                    _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.CompanyRibbonView);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, navPath, NavigationComplete);
                }

                if (navPath.Contains("Product"))
                {
                    _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.ProductRibbonView);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, navPath, NavigationComplete);
                }
            }
        }

        private void NavigationComplete(NavigationResult result)
        {
            Log.Debug(result.Context.Uri.ToString());
            //Log.Error(result.Error.Message);
            Log.Debug(result.Context.NavigationService.ToString());
        }

        //public override void OnNavigatedFrom(NavigationContext navigationContext)
        //{
        //    ApplicationCommands.OrdersNavigateCommand.UnregisterCommand(NavigateCommand);
        //}
    }
}
