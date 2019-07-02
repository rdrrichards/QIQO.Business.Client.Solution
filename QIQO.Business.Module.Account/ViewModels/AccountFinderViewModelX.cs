using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Module.Account.Views;
using QIQO.Business.Module.General.Models;
using System;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountFinderViewModelX : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<BusinessItem> _accounts = new ObservableCollection<BusinessItem>();
        private readonly string _viewTitle = "Account Find";
        private string _searchTerm;
        private object _selectedItem;
        private bool _isSearching;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                ChooseItemCommand.RaiseCanExecuteChanged();
            }
        }
        public int SelectedItemIndex { get; set; }

        public AccountFinderViewModelX(IEventAggregator eventAggregator, IServiceFactory serviceFactory, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _serviceFactory = serviceFactory;
            _regionManager = regionManager;
            BindCommands();
        }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand ChooseItemCommand { get; set; }

        public override string ViewTitle { get { return _viewTitle; } }

        public ObservableCollection<BusinessItem> FoundItems
        {
            get { return _accounts; }
            private set { SetProperty(ref _accounts, value); }
        }

        public string SearchTerm
        {
            get { return _searchTerm; }
            set { SetProperty(ref _searchTerm, value); }
        }

        public bool IsLoading
        {
            get { return _isSearching; }
            private set { SetProperty(ref _isSearching, value); }
        }

        private void BindCommands()
        {
            SearchCommand = new DelegateCommand(GetAccounts);
            ChooseItemCommand = new DelegateCommand(ChooseAccount);
        }

        private async void GetAccounts()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                IsBusy = true;
                IsLoading = true;
                var account_service = _serviceFactory.CreateClient<IAccountService>();

                using (account_service)
                {
                    try
                    {
                        var results = account_service.FindAccountByCompanyAsync((Company)CurrentCompany, SearchTerm);
                        await results;

                        foreach (var account in results.Result)
                        {
                            FoundItems.Add(Map(account));
                        }

                        MessageToDisplay = FoundItems.Count.ToString() + " account(s) found";
                    }
                    catch (Exception ex)
                    {
                        MessageToDisplay = ex.Message;
                        return;
                    }
                }
            }
            else
            {
                MessageToDisplay = "You must enter a search term in order to find an account";
            }
            IsLoading = false;
            IsBusy = false;
        }

        private void ChooseAccount()
        {
            if (SelectedItem is BusinessItem busItem)
            {
                if (busItem.BusinessObject is Client.Entities.Account account)
                {
                    var parameters = new NavigationParameters { { "AccountKey", account.AccountKey } };
                    _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(AccountViewX).FullName, parameters);
                }
            }
        }

        private BusinessItem Map(Client.Entities.Account account)
        {
            return new BusinessItem
            {
                ItemType = account.AccountType.ToString(),
                ItemCode = account.AccountCode,
                ItemName = account.AccountName,
                ItemEntryDate = account.AccountStartDate,
                BusinessObject = account
            };
        }
    }
}
