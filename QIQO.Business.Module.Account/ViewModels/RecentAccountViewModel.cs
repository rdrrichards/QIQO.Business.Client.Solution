using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Wrappers;
using QIQO.Business.Module.Account.Interfaces;
using QIQO.Business.Module.General.Models;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class RecentAccountViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IRecentAccountsService _recentAccountsService;
        private readonly IEventAggregator _eventAggregator;
        private string _headerMsg = "Recent Accounts";
        private ObservableCollection<BusinessItem> _accounts = new ObservableCollection<BusinessItem>();
        private object _selectedItem;

        public RecentAccountViewModel(IRegionManager regionManager, IRecentAccountsService recentAccountsService, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _recentAccountsService = recentAccountsService;
            _eventAggregator = eventAggregator;

            InitializeAccounts();

            ChooseItemCommand = new DelegateCommand(GotoAccount);
            _eventAggregator.GetEvent<RecentAccountServiceEvent>().Subscribe(InitializeAccounts);
        }

        private void GotoAccount()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is AccountWrapper account)
                {
                    var parameters = new NavigationParameters();
                    parameters.Add("AccountKey", account.AccountKey);
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.AccountViewX, parameters);
                }
            }
        }

        private void InitializeAccounts(int blah = 0)
        {
            foreach(var wrap in _recentAccountsService.GetRecentAccounts())
                RecentAccountItems.Add(Map(wrap));
        }

        public DelegateCommand ChooseItemCommand { get; set; }
        public int SelectedItemIndex { get; set; }
        public bool IsLoading => false;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public ObservableCollection<BusinessItem> RecentAccountItems
        {
            get { return _accounts; }
            private set { SetProperty(ref _accounts, value); }
        }

        public string HeaderMessage
        {
            get { return _headerMsg; }
            private set { SetProperty(ref _headerMsg, value); }
        }

        private BusinessItem Map(AccountWrapper account)
        {
            return new BusinessItem
            {
                ItemCode = account.AccountCode,
                ItemName = account.AccountName,
                BusinessObject = account
            };
        }
    }
}
