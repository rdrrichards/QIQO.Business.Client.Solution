using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using CommonServiceLocator;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using QIQO.Business.Client.Core.Infrastructure;
using System.Transactions;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountViewModel : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IRegionManager region_manager;
        private readonly IProductListService product_service;
        private readonly IStateListService address_postal_service;
        private readonly IReportService report_service;
        private readonly IAccountEntityService account_entity_service;
        private AccountWrapper _curr_account;
        private ObservableCollection<AddressPostal> _states;
        private AddressWrapper _def_billing;
        private AddressWrapper _def_mailing;
        private AddressWrapper _def_shipping;
        private string _account_code_holder;
        private object _currentSelectedAttribute;
        private object _currentSelectedFeeSchedule;
        private ObservableCollection<Product> _productlist;
        private object _currentSelectedEmployee;
        private object _currentSelectedContact;

        public AccountViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry, 
            IRegionManager regionManager, IProductListService product_svc, IStateListService address_postal_serv,
            IReportService reportService, IAccountEntityService account_entity_svc)
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            region_manager = regionManager;
            product_service = product_svc;
            address_postal_service = address_postal_serv;
            report_service = reportService;
            account_entity_service = account_entity_svc;

            GetProductList();
            GetStateList();
            BindCommands();
            InitNewAccount();
            //Account.PropertyChanged += Context_PropertyChanged;
            RegisterApplicationCommands();
            //ApplicationCommands.DeleteAccountCommand.RegisterCommand(DeleteAccountCommand);
            event_aggregator.GetEvent<AccountLoadedEvent>().Publish(string.Empty);
        }

        private void InitNewAccount()
        {
            Account = new AccountWrapper(account_entity_service.InitNewAccount(CurrentCompanyKey));
            //Client.Entities.Account _account = new Client.Entities.Account() { CompanyKey = CurrentCompanyKey };

            //// Think about refactoring this out into it own method
            //Account = new AccountWrapper(_account);
            DefaultShippingAddress = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Shipping).FirstOrDefault();
            DefaultBillingAddress = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Billing).FirstOrDefault();
            DefaultMailingAddress = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Mailing).FirstOrDefault();
            //GetAttributeList();
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (Account.IsChanged && !string.IsNullOrWhiteSpace(Account.AccountCode))
            {
                Confirmation confirm = new Confirmation();
                confirm.Title = ApplicationStrings.SaveChangesTitle;
                confirm.Content = ApplicationStrings.SaveChangesPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r => {
                        if (r != null && r.Confirmed)
                        {
                            if (Account.IsValid)
                            {
                                DoSave();
                                continuationCallback(false);
                            }
                            continuationCallback(true);
                        }
                        else
                        {
                            continuationCallback(true);
                        }
                    });
            }
            else
                continuationCallback(true);
        }

        public bool KeepAlive { get; } = false;

        private void FindAccount()
        {
            ItemSelectionNotification notification = new ItemSelectionNotification();
            notification.Title = ApplicationStrings.NotificationFindAccount;
            FindAccountRequest.Raise(notification,
                r => {
                    if (r != null && r.Confirmed && r.SelectedItem != null)
                    {
                        Client.Entities.Account found_account = r.SelectedItem as Client.Entities.Account;
                        if (found_account != null)
                            GetAccount(found_account.AccountKey);
                    }
                }); //)
        }

        public override string ViewTitle { get { return "Accounts"; } }

        public AccountWrapper Account
        {
            get { return _curr_account; }
            private set { SetProperty(ref _curr_account, value); }
        }

        public AddressWrapper DefaultBillingAddress //{ get; set; }
        {
            get { return _def_billing; }
            private set { SetProperty(ref _def_billing, value); }
        }
        public AddressWrapper DefaultShippingAddress //{ get; set; }
        {
            get { return _def_shipping; }
            private set { SetProperty(ref _def_shipping, value); }
        }
        public AddressWrapper DefaultMailingAddress //{ get; set; }
        {
            get { return _def_mailing; }
            private set { SetProperty(ref _def_mailing, value); }
        }
        public object FeeSelectedItem
        {
            get { return _currentSelectedFeeSchedule; }
            set { SetProperty(ref _currentSelectedFeeSchedule, value); InvalidateCommands(); }
        }
        public int FeeSelectedIndex { get; set; }
        public object AttSelectedItem
        {
            get { return _currentSelectedAttribute; }
            set { SetProperty(ref _currentSelectedAttribute, value); InvalidateCommands(); }
        }
        public int AttSelectedIndex { get; set; }


        public ObservableCollection<Product> ProductList
        {
            get { return _productlist; }
            private set { SetProperty(ref _productlist, value); }
        }
        public object EmpSelectedItem
        {
            get { return _currentSelectedEmployee; }
            set
            {
                if (value != _currentSelectedEmployee)
                {
                    _currentSelectedEmployee = value;
                    RaisePropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int EmpSelectedIndex { get; set; }
        public object ContactSelectedItem
        {
            get { return _currentSelectedContact; }
            set
            {
                if (value != _currentSelectedContact)
                {
                    _currentSelectedContact = value;
                    RaisePropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int ContactSelectedIndex { get; set; }

        public List<string> CompanyRoles
        {
            get
            {
                return new List<string>(new string[] { Roles.QIQORoleEmployee, Roles.QIQORoleManager, Roles.QIQORoleOwner });
            }
        }

        public ObservableCollection<AddressPostal> States
        {
            get { return _states; }
            private set { SetProperty(ref _states, value); }
        }

        public async void ValidateAddress(AddressWrapper which_address)
        {
            IAddressService addr_service = service_factory.CreateClient<IAddressService>();
            using (addr_service)
            {
                try
                {
                    Task<AddressPostal> task = addr_service.GetAddressInfoByPostalAsync(which_address.AddressPostalCode);
                    await task;
                    AddressPostal postal_info = task.Result;

                    if (postal_info != null)
                    {
                        which_address.AddressCity = postal_info.CityName;
                        which_address.AddressState = postal_info.StateCode;
                        which_address.AddressCounty = postal_info.CountyName;
                        which_address.AddressCountry = postal_info.CountryName;
                    }
                    else
                    {
                        event_aggregator.GetEvent<AccountUpdatedEvent>().Publish("Postal code not found");
                    }
                }
                catch (Exception ex)
                {
                    event_aggregator.GetEvent<AccountUpdatedEvent>().Publish(ex.Message);
                    return;
                }
            }
        }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }

        //public RelayCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand PrintCommand { get; set; }
        public DelegateCommand<string> GetAccountCommand { get; set; }
        public DelegateCommand GenAccountNumCommand { get; set; }
        public DelegateCommand<AddressWrapper> ValidateAddressCommand { get; set; }
        public DelegateCommand<object> NewOrderCommand { get; set; }

        public DelegateCommand NewFeeScheuleCommand { get; set; }
        public DelegateCommand EditFeeScheuleCommand { get; set; }
        public DelegateCommand DeleteFeeScheuleCommand { get; set; }

        public DelegateCommand NewAttributeCommand { get; set; }
        public DelegateCommand EditAttributeCommand { get; set; }
        public DelegateCommand DeleteAttributeCommand { get; set; }
        public DelegateCommand FindAccountCommand { get; set; }
        public DelegateCommand EditEmployeeCommand { get; set; }
        public DelegateCommand AddEmployeeCommand { get; set; }
        public DelegateCommand DeleteEmployeeCommand { get; set; }
        public DelegateCommand EditContactCommand { get; set; }
        public DelegateCommand AddContactCommand { get; set; }
        public DelegateCommand DeleteContactCommand { get; set; }

        public InteractionRequest<ItemSelectionNotification> FindAccountRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditFeeScheduledRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditAttributeRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditAccountPersonRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditContactRequest { get; set; }
        public InteractionRequest<IConfirmation> SaveChangesConfirmationRequest { get; set; }
        public InteractionRequest<IConfirmation> DeleteConfirmationRequest { get; set; }


        private void BindCommands()
        {
            SaveCommand = new DelegateCommand(DoSave, CanSave);
            DeleteCommand = new DelegateCommand(DoDelete, CanDoDelete);
            CancelCommand = new DelegateCommand(DoCancel, CanDoCancel);
            PrintCommand = new DelegateCommand(DoPrint, CanDoDelete);

            FindAccountCommand = new DelegateCommand(FindAccount);
            GetAccountCommand = new DelegateCommand<string>(LoadAccount);
            GenAccountNumCommand = new DelegateCommand(GetNextAccountNumber);
            ValidateAddressCommand = new DelegateCommand<AddressWrapper>(ValidateAddress);
            NewOrderCommand = new DelegateCommand<object>(StartNewOrder, CanStartNewOrder);

            NewFeeScheuleCommand = new DelegateCommand(AddFeeSchedule);
            EditFeeScheuleCommand = new DelegateCommand(EditFeeSchedule, CanEditFeeSchedule);
            DeleteFeeScheuleCommand = new DelegateCommand(DeleteFeeScheule, CanDeleteFeeScheule);

            NewAttributeCommand = new DelegateCommand(AddAttribute);
            EditAttributeCommand = new DelegateCommand(EditAttribute, CanEditAttribute);
            DeleteAttributeCommand = new DelegateCommand(DeleteAttribute, CanDeleteAttribute);

            FindAccountRequest = new InteractionRequest<ItemSelectionNotification>();
            EditFeeScheduledRequest = new InteractionRequest<ItemEditNotification>();
            EditAttributeRequest = new InteractionRequest<ItemEditNotification>();
            EditAccountPersonRequest = new InteractionRequest<ItemEditNotification>();
            EditContactRequest = new InteractionRequest<ItemEditNotification>();
            SaveChangesConfirmationRequest = new InteractionRequest<IConfirmation>();
            DeleteConfirmationRequest = new InteractionRequest<IConfirmation>();

            EditEmployeeCommand = new DelegateCommand(EditEmployee, CanEditEmployee);
            DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee, CanDeleteEmployee);
            AddEmployeeCommand = new DelegateCommand(AddEmployee);

            EditContactCommand = new DelegateCommand(EditContact, CanEditContact);
            DeleteContactCommand = new DelegateCommand(DeleteContact, CanDeleteContact);
            AddContactCommand = new DelegateCommand(AddContact);
        }

        private void DoPrint()
        {
            report_service.ExecuteReport(Reports.Account, $"account_key={Account.AccountKey.ToString()}");
        }

        private bool CanDoCancel()
        {
            return Account.IsChanged && Account.IsValid;
        }

        private void DoCancel()
        {
            if (Account.IsChanged && !string.IsNullOrWhiteSpace(Account.AccountCode))
            {
                Confirmation confirm = new Confirmation();
                confirm.Title = ApplicationStrings.ConfirmCancelTitle;
                confirm.Content = ApplicationStrings.ConfirmCancelPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r => {
                        if (r != null && r.Confirmed)
                        {
                            DoSave();
                        }
                        InitNewAccount();
                    });
            }
        }

        private bool CanDoDelete()
        {
            return Account.AccountKey > 0;
        }

        private void DoDelete()
        {
            Confirmation confirm = new Confirmation();
            confirm.Title = ApplicationStrings.DeleteAccountTitle;
            confirm.Content = $"Are you sure you want to delete account {Account.AccountName}?\n\nClick OK to delete\nClick Cancel to return to the form.";
            DeleteConfirmationRequest.Raise(confirm,
                r => {
                    if (r != null && r.Confirmed)
                    {
                        DeleteAccount();
                    }
                });
        }

        private void DeleteAccount()
        {
            ExecuteFaultHandledOperation(() =>
            {
                IAccountService account_service = service_factory.CreateClient<IAccountService>();
                using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    using (account_service)
                    {
                        bool ret_val = account_service.DeleteAccount(Account.Model);
                        event_aggregator.GetEvent<AccountDeletedEvent>().Publish($"{Account.AccountCode} - {Account.AccountName} deleted sucessfully");
                        InitNewAccount();
                    }
                    scope.Complete();
                }
            });
        }

        private void RegisterApplicationCommands()
        {
            ApplicationCommands.NewOrderNavigateCommand.RegisterCommand(NewOrderCommand);

            ApplicationCommands.SaveAccountCommand.RegisterCommand(SaveCommand);
            ApplicationCommands.FindAccountCommand.RegisterCommand(FindAccountCommand);
            ApplicationCommands.DeleteAccountCommand.RegisterCommand(DeleteCommand);
            ApplicationCommands.CancelAccountCommand.RegisterCommand(CancelCommand);
            ApplicationCommands.PrintAccountCommand.RegisterCommand(PrintCommand);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ApplicationCommands.NewOrderNavigateCommand.UnregisterCommand(NewOrderCommand);

            ApplicationCommands.SaveAccountCommand.UnregisterCommand(SaveCommand);
            ApplicationCommands.FindAccountCommand.UnregisterCommand(FindAccountCommand);
            ApplicationCommands.DeleteAccountCommand.UnregisterCommand(DeleteCommand);
            ApplicationCommands.CancelAccountCommand.UnregisterCommand(CancelCommand);
            ApplicationCommands.PrintAccountCommand.UnregisterCommand(PrintCommand);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var paramAccountType = navigationContext.Parameters.Where(item => item.Key == "AccountType").FirstOrDefault();

            if (paramAccountType.Value != null)
            {
                InitNewAccount();
                Account.AccountType = (QIQOAccountType)paramAccountType.Value;
                Account.PropertyChanged += Context_PropertyChanged;
                RegisterApplicationCommands();
                InvalidateCommands();
                return;
            }
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            EditFeeScheuleCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            PrintCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            EditAttributeCommand.RaiseCanExecuteChanged();
            DeleteAttributeCommand.RaiseCanExecuteChanged();
            DeleteFeeScheuleCommand.RaiseCanExecuteChanged();
            NewOrderCommand.RaiseCanExecuteChanged();
            EditContactCommand.RaiseCanExecuteChanged();
            DeleteContactCommand.RaiseCanExecuteChanged();
            EditEmployeeCommand.RaiseCanExecuteChanged();
            DeleteEmployeeCommand.RaiseCanExecuteChanged();
        }

        private bool CanDeleteAttribute()
        {
            return AttSelectedItem != null;
        }

        private void DeleteAttribute()
        {
            var att_to_remove = AttSelectedItem as EntityAttributeWrapper;
            if (att_to_remove != null) att_to_remove.AttributeValue = ""; //Account.AccountAttributes.Remove(att_to_remove);
        }

        private bool CanEditAttribute()
        {
            return AttSelectedItem != null;
        }

        private void EditAttribute()
        {
            var att_to_edit = AttSelectedItem as EntityAttributeWrapper;
            if (att_to_edit != null)
            {
                EntityAttribute att_copy = att_to_edit.Model.Copy();
                ChangeAttribute(att_copy, ApplicationStrings.NotificationEdit);
            }
        }

        private void AddAttribute()
        {
            var att_to_edit = new EntityAttribute();
            ChangeAttribute(att_to_edit, ApplicationStrings.NotificationAdd);
        }

        private void ChangeAttribute(EntityAttribute attribute, string action)
        {
            var att_to_edit = attribute as EntityAttribute;
            if (att_to_edit != null)
            {
                ItemEditNotification notification = new ItemEditNotification(att_to_edit);
                notification.Title = action + " Attribute"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditAttributeRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            EntityAttribute att = r.EditibleObject as EntityAttribute;
                            if (att != null)
                            {
                                var att_to_change = AttSelectedItem as EntityAttributeWrapper;
                                if (att_to_change != null)
                                {
                                    att_to_change.AttributeValue = att.AttributeValue;
                                }
                            }
                        }
                    });
            }
        }

        private bool CanDeleteFeeScheule()
        {
            return FeeSelectedItem != null;
        }

        private void DeleteFeeScheule()
        {
            var fee_to_remove = FeeSelectedItem as FeeScheduleWrapper;
            if (fee_to_remove != null)
            {
                if (fee_to_remove.FeeScheduleKey != 0)
                    fee_to_remove.FeeScheduleEndDate = DateTime.Today;
                else
                    Account.FeeSchedules.Remove(fee_to_remove);
            }
        }

        private bool CanEditFeeSchedule()
        {
            return FeeSelectedItem != null;
        }

        private void EditFeeSchedule()
        {
            var fee_to_edit = FeeSelectedItem as FeeScheduleWrapper;
            if (fee_to_edit != null)
            {
                FeeSchedule fee_copy = fee_to_edit.Model.Copy();
                ChangeFeeSchedule(fee_copy, ApplicationStrings.NotificationEdit);
            }
        }

        private void AddFeeSchedule()
        {
            var fee_to_edit = new FeeSchedule()
            {
                FeeScheduleStartDate = DateTime.Today,
                FeeScheduleEndDate = DateTime.Today.AddYears(5),
                FeeScheduleTypeCode = "F",
                CompanyKey = CurrentCompanyKey,
                AccountKey = Account.AccountKey
            };
            ChangeFeeSchedule(fee_to_edit, ApplicationStrings.NotificationAdd);
        }

        private void ChangeFeeSchedule(FeeSchedule schedule, string action)
        {
            var fee_to_edit = schedule as FeeSchedule;
            if (fee_to_edit != null)
            {
                ItemEditNotification notification = new ItemEditNotification(fee_to_edit);
                notification.Title = action + " Fee Schedule"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditFeeScheduledRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            FeeSchedule fee = r.EditibleObject as FeeSchedule;
                            if (fee != null)
                            {
                                // We need to actually add the fee shedule to something
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var fee_to_change = FeeSelectedItem as FeeScheduleWrapper;
                                    if (fee_to_change != null)
                                    {
                                        //fee_to_change.CompanyKey = fee.CompanyKey;
                                        //fee_to_change.FeeScheduleKey = fee.FeeScheduleKey;
                                        fee_to_change.FeeScheduleStartDate = fee.FeeScheduleStartDate;
                                        fee_to_change.FeeScheduleEndDate = fee.FeeScheduleEndDate;
                                        fee_to_change.FeeScheduleTypeCode = fee.FeeScheduleTypeCode;
                                        fee_to_change.FeeScheduleValue = fee.FeeScheduleValue;
                                        //fee_to_change.AccountKey = fee.AccountKey;
                                        //fee_to_change.Product.ProductCode = fee.Product.ProductCode;
                                        //fee_to_change.Product.ProductDesc = fee.Product.ProductDesc;
                                        //fee_to_change.Product.ProductKey = fee.Product.ProductKey;
                                        fee_to_change.ProductCode = fee.ProductCode;
                                        fee_to_change.ProductDesc = fee.ProductDesc;
                                        fee_to_change.ProductKey = fee.ProductKey;
                                    }
                                }
                                else
                                {
                                    Account.FeeSchedules.Add(new FeeScheduleWrapper(fee));
                                }
                            }
                        }
                    });
            }
        }
        private bool CanEditEmployee()
        {
            return EmpSelectedItem != null;
        }

        private void EditEmployee()
        {
            var emp_to_edit = EmpSelectedItem as AccountPersonWrapper;
            if (emp_to_edit != null)
            {
                AccountPerson ap_copy = emp_to_edit.Model.Copy();
                ChangeEmployee(ap_copy, ApplicationStrings.NotificationEdit);
            }
        }

        private void AddEmployee()
        {
            var emp_to_edit = new AccountPerson()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddYears(5),
                Comment = "New account contact",
                CompanyRoleType = QIQOPersonType.AccountEmployee,
                RoleInCompany = "Account Contact"
            };
            ChangeEmployee(emp_to_edit, ApplicationStrings.NotificationAdd);
        }

        private void ChangeEmployee(AccountPerson employee, string action)
        {
            var emp_to_edit = employee as AccountPerson;
            if (emp_to_edit != null)
            {
                ItemEditNotification notification = new ItemEditNotification(emp_to_edit);
                notification.Title = action + " Account Employee"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditAccountPersonRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            AccountPerson emp = r.EditibleObject as AccountPerson;
                            if (emp != null)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var emp_to_change = EmpSelectedItem as AccountPersonWrapper;
                                    if (emp_to_change != null)
                                    {
                                        emp_to_change.Comment = emp.Comment;
                                        emp_to_change.CompanyRoleType = emp.CompanyRoleType;
                                        emp_to_change.EndDate = emp.EndDate;
                                        emp_to_change.StartDate = emp.StartDate;
                                        emp_to_change.PersonCode = emp.PersonCode;
                                        emp_to_change.PersonDOB = emp.PersonDOB;
                                        emp_to_change.PersonFirstName = emp.PersonFirstName;
                                        emp_to_change.PersonLastName = emp.PersonLastName;
                                        emp_to_change.PersonMI = emp.PersonMI;
                                        emp_to_change.RoleInCompany = emp.RoleInCompany;
                                    }
                                }
                                else
                                {
                                    Account.Employees.Add(new AccountPersonWrapper(emp));
                                }
                            }
                        }
                    });
            }
        }

        private bool CanDeleteEmployee()
        {
            return EmpSelectedItem != null;
        }

        private void DeleteEmployee()
        {
            var emp_to_remove = EmpSelectedItem as AccountPersonWrapper;
            if (emp_to_remove != null)
            {
                if (emp_to_remove.PersonKey != 0)
                    emp_to_remove.EndDate = DateTime.Today;
                else
                    Account.Employees.Remove(emp_to_remove);
            }
        }


        private bool CanEditContact()
        {
            return ContactSelectedItem != null;
        }

        private void EditContact()
        {
            var cont_to_edit = ContactSelectedItem as ContactWrapper;
            if (cont_to_edit != null)
            {
                Contact cnt_copy = cont_to_edit.Model.Copy();
                ChangeContact(cnt_copy, ApplicationStrings.NotificationEdit);
            }
        }

        private void AddContact()
        {
            var cont_to_edit = new Contact()
            {
                EntityKey = Account.AccountKey,
                EntityTypeKey = (int)QIQOEntityType.Account,
                ContactActiveFlg = 1,
                ContactDefaultFlg = 0,
                ContactTypeKey = (int)QIQOContactType.AccountContact,
                ContactType = QIQOContactType.AccountContact,
                ContactValue = ""
            };
            ChangeContact(cont_to_edit, ApplicationStrings.NotificationAdd);
        }

        private void ChangeContact(Contact contact, string action)
        {
            var emp_to_edit = contact as Contact;
            if (emp_to_edit != null)
            {
                ItemEditNotification notification = new ItemEditNotification(emp_to_edit);
                notification.Title = action + " Contact";
                EditContactRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            Contact cnt = r.EditibleObject as Contact;
                            if (cnt != null)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var cnt_to_change = ContactSelectedItem as ContactWrapper;
                                    if (cnt_to_change != null)
                                    {
                                        cnt_to_change.ContactActiveFlg = cnt.ContactActiveFlg;
                                        cnt_to_change.ContactDefaultFlg = cnt.ContactDefaultFlg;
                                        cnt_to_change.ContactTypeKey = cnt.ContactTypeKey;
                                        cnt_to_change.ContactType = cnt.ContactType;
                                        cnt_to_change.ContactValue = cnt.ContactValue;
                                    }
                                }
                                else
                                {
                                    Account.Contacts.Add(new ContactWrapper(cnt));
                                }
                            }
                        }
                    });
            }
        }

        private bool CanDeleteContact()
        {
            return ContactSelectedItem != null;
        }

        private void DeleteContact()
        {
            var cont_to_remove = ContactSelectedItem as ContactWrapper;
            if (cont_to_remove != null)
                Account.Contacts.Remove(cont_to_remove);
        }

        private bool CanStartNewOrder(object obj)
        {
            return Account?.AccountKey > 0;
        }

        private void StartNewOrder(object navigatePath)
        {
            // See about opening a new tab in the Order sheet and auto populating the account number
            var parameters = new NavigationParameters();
            parameters.Add("AccountCode", Account.AccountCode);
            region_manager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OrderView, NavigationComplete, parameters);
            //event_aggregator.GetEvent<AccountNewOrderEvent>().Publish(Account.AccountCode);
        }

        private void NavigationComplete(NavigationResult result){ }

        private bool CanSave()
        {
            return Account.IsChanged && Account.IsValid;
        }

        private void DoSave()
        {
            ExecuteFaultHandledOperation(() =>
            {
                event_aggregator.GetEvent<AccountUpdatedEvent>().Publish(ApplicationStrings.BeginningSave);
                ICleaningUtility cleaner = ServiceLocator.Current.GetInstance<ICleaningUtility>();

                using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    if (Account.AccountKey == 0 && Account.AccountCode == "")
                    {
                        GetNextAccountNumber();
                    }

                    List<EntityAttributeWrapper> atts_with_values = Account.AccountAttributes.Where(i => i.AttributeValue != "" || i.AttributeKey > 0).ToList();
                    Account.AccountAttributes.Clear();
                    foreach (var good_att in atts_with_values)
                        Account.AccountAttributes.Add(good_att);

                    Account.Model.Addresses.Clear();
                    cleaner.CleanAddress(DefaultBillingAddress.Model);
                    DefaultBillingAddress.AddressType = QIQOAddressType.Billing;
                    Account.Model.Addresses.Add(DefaultBillingAddress.Model);
                    cleaner.CleanAddress(DefaultShippingAddress.Model);
                    DefaultShippingAddress.AddressType = QIQOAddressType.Shipping;
                    Account.Model.Addresses.Add(DefaultShippingAddress.Model);
                    cleaner.CleanAddress(DefaultMailingAddress.Model);
                    DefaultMailingAddress.AddressType = QIQOAddressType.Mailing;
                    Account.Model.Addresses.Add(DefaultMailingAddress.Model);

                    IAccountService account_service = service_factory.CreateClient<IAccountService>();
                    using (account_service)
                    {
                        int _account_key = account_service.CreateAccount(Account.Model);
                        event_aggregator.GetEvent<AccountUpdatedEvent>().Publish($"{Account.AccountCode} - {Account.AccountName} updated sucessfully");
                        Account.AcceptChanges();
                    }
                    scope.Complete();
                }
            });
        }

        private void GetNextAccountNumber()
        {
            ICompanyService company_service = service_factory.CreateClient<ICompanyService>();
            Account.AccountCode = company_service.GetCompanyNextNumber((Company)CurrentCompany, QIQOEntityNumberType.AccountNumber);
            _account_code_holder = Account.AccountCode;
        }

        private void LoadAccount(string parameter)
        {
            // Really, we need to add validation that the code doesn't already exist for the account and do something else if it does.
            if (parameter != null && parameter != _account_code_holder && parameter != "")
            {
                string acct_code = parameter;
                var curr_co = CurrentCompany as Company;

                ExecuteFaultHandledOperation(() =>
                {
                    IAccountService account_service = service_factory.CreateClient<IAccountService>();
                    using (account_service)
                    {
                        Client.Entities.Account _account = account_service.GetAccountByCode(acct_code, curr_co.CompanyCode);

                        if (_account != null)
                        {
                            MapAccount(_account);
                        }
                    }

                    GetAttributeList();
                });
            }
        }

        private void GetAccount(int account_key)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IAccountService account_service = service_factory.CreateClient<IAccountService>();
                using (account_service)
                {
                    Client.Entities.Account _account = account_service.GetAccountByID(account_key, true);

                    if (_account != null)
                    {
                        MapAccount(_account);
                    }

                    GetAttributeList();
                }
            });
        }

        private void GetProductList() //Task<ObservableCollection<Product>>
        {
            ProductList = new ObservableCollection<Product>(product_service.ProductList);
        }

        private void MapAccount(Client.Entities.Account _account)
        {
            if (_account != null)
            {
                _account_code_holder = _account.AccountCode;

                Account = new AccountWrapper(_account);

                AddressWrapper ship_addy = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Shipping).FirstOrDefault();
                if (ship_addy != null)
                    DefaultShippingAddress = ship_addy; // new AddressWrapper(ship_addy);
                else
                    DefaultShippingAddress = new AddressWrapper(new Address() { AddressType = QIQOAddressType.Shipping });

                AddressWrapper bill_addy = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Billing).FirstOrDefault();
                if (bill_addy != null)
                    DefaultBillingAddress = bill_addy; // new AddressWrapper(bill_addy);
                else
                    DefaultBillingAddress = new AddressWrapper(new Address() { AddressType = QIQOAddressType.Billing });

                AddressWrapper mail_addy = Account.Addresses.Where(type => type.AddressType == QIQOAddressType.Mailing).FirstOrDefault();
                if (mail_addy != null)
                    DefaultMailingAddress = mail_addy; // new AddressWrapper(mail_addy);
                else
                    DefaultMailingAddress = new AddressWrapper(new Address() { AddressType = QIQOAddressType.Mailing });
            }
        }

        public void PopulateProductInfo()
        {
            if (this.ProductList != null)
            {
                //OrderItem var = SelectedOrderItem as OrderItem;
                FeeSchedule var = Account.FeeSchedules[FeeSelectedIndex].Model;
                if (var != null && var.ProductKey > 0)
                {
                    var sp = ProductList.Where(item => item.ProductKey == var.ProductKey).FirstOrDefault();

                    if (sp.ProductName != "")
                    {
                        var rp = sp.ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODBASE).FirstOrDefault();
                        //var dq = sp[0].ProductAttributes.Where(item => item.AttributeType == QIQOAttributeType.Product_PRODDFQTY).ToList();

                        //var.ProductKey = sp[0].ProductKey;
                        //var.Product = sp;
                        var.AccountKey = Account.AccountKey;
                        var.FeeScheduleTypeCode = "F";
                        var.FeeScheduleValue = decimal.Parse(rp.AttributeValue);
                        var.FeeScheduleStartDate = DateTime.Today;
                        var.FeeScheduleEndDate = DateTime.Today.AddYears(2);
                        var.ProductCode = sp.ProductCode;
                        var.ProductDesc = sp.ProductDesc;
                        RaisePropertyChanged("FeeSchedules");
                    }
                }
            }
        }

        private void GetAttributeList()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ITypeService type_service = service_factory.CreateClient<ITypeService>();
                using (type_service)
                {
                    List<AttributeType> atttype_list = type_service.GetAttributeTypeList();
                    //ObservableCollection<AttributeType> atttypes = new ObservableCollection<AttributeType>(atttype_list);
                    var acct_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Account").ToList();
                    var gcnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "General Contact").ToList();
                    var acnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Account Contact").ToList();

                    var all_atts = acct_atts.Concat(gcnt_atts.Concat(acnt_atts));

                    List<AttributeType>  available_attribute_types = new List<AttributeType>(all_atts);
                    LoadEntityAttrbuteList(available_attribute_types);
                    Account.PropertyChanged += Context_PropertyChanged;
                    InvalidateCommands();
                }
            });
        }

        private void LoadEntityAttrbuteList(List<AttributeType> available_attribute_types)
        {
            if (available_attribute_types != null)
            {
                if (available_attribute_types.Count > 0)
                {
                    foreach (AttributeType attype in available_attribute_types)
                    {
                        EntityAttribute ent_att = new EntityAttribute()
                        {
                            AttributeDataTypeKey = (int)attype.AttributeDataTypeKey,
                            AttributeDisplayFormat = attype.AttributeDefaultFormat,
                            AttributeKey = 0,
                            AttributeType = (QIQOAttributeType)attype.AttributeTypeKey,
                            AttributeValue = "",
                            EntityKey = Account != null ? Account.AccountKey : 0,
                            EntityType = QIQOEntityType.Account,
                            AttributeTypeData = attype,
                            AttributeDataType = attype.AttributeDataTypeKey,
                            EntityTypeData = new EntityType()
                        };

                        var att = Account.AccountAttributes.Where(item => item.AttributeType == ent_att.AttributeType).FirstOrDefault();

                        if (att == null)
                            Account.AccountAttributes.Add(new EntityAttributeWrapper(ent_att));
                    }
                }
            }
            //Account.AcceptChanges();
        }

        private void GetStateList()
        {
            States = new ObservableCollection<AddressPostal>(address_postal_service.StateList);
        }
    }
}
