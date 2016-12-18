using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;

namespace QIQO.Business.Module.Company.ViewModels
{
    public class CompanyViewModel : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IRegionManager _regionManager;
        private readonly IStateListService address_postal_service;
        private readonly Client.Entities.Company _currentCoObject;
        private AddressWrapper _def_billing;
        private AddressWrapper _def_shipping;

        public CompanyViewModel(IEventAggregator event_aggtr, IServiceFactory service_fctry, IRegionManager region_manager, IStateListService address_postal_serv)
        {
            event_aggregator = event_aggtr;
            service_factory = service_fctry;
            _regionManager = region_manager;
            address_postal_service = address_postal_serv;

            _currentCoObject = CurrentCompany as Client.Entities.Company;

            BindCommands();
            GetStateList();
            GetEmployeeList();
            MapObjectToProps(_currentCoObject);
            //GetAttributeList();
            RegisterApplicationCommands();
            event_aggregator.GetEvent<CompanyLoadedEvent>().Publish("Company data loaded sucessfully");
            InvalidateCommands();
        }

        public bool KeepAlive { get; } = false;

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (Company.IsChanged) // && !string.IsNullOrWhiteSpace(Company.CompanyCode))
            {
                Confirmation confirm = new Confirmation();
                confirm.Title = ApplicationStrings.SaveChangesTitle;
                confirm.Content = ApplicationStrings.SaveChangesPrompt;
                SaveChangesConfirmationRequest.Raise(confirm,
                    r => {
                        if (r != null && r.Confirmed)
                        {
                            if (Company.IsValid)
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

        public override string ViewTitle { get { return "Company"; } }

        public CompanyWrapper Company { get; set; }

        private ObservableCollection<AddressWrapper> _company_addresses;
        private ObservableCollection<AddressPostal> _states;
        private object _currentSelectedEmployee;
        private object _currentSelectedAttribute;
        private object _currentSelectedChartOfAccount;

        public List<string> AccountTypes { get; protected set; }
        public List<string> BalanceTypes { get; protected set; }

        public ObservableCollection<AddressWrapper> CompanyAddresses
        {
            get { return _company_addresses; }
            set
            {
                if (value != _company_addresses)
                {
                    _company_addresses = value;
                    OnPropertyChanged();
                    InvalidateCommands();
                }
            }
        }

        public ObservableCollection<AddressPostal> States
        {
            get { return _states; }
            private set { SetProperty(ref _states, value); }

        }

        public List<string> CompanyRoles
        {
            get
            {
                return new List<string>(new string[] { Roles.QIQORoleEmployee, Roles.QIQORoleManager, Roles.QIQORoleOwner });
            }
        }

        //public AddressWrapper DefaultBillingAddress { get; set; }
        //public AddressWrapper DefaultShippingAddress { get; set; }

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

        public object COASelectedItem
        {
            get { return _currentSelectedChartOfAccount; }
            set
            {
                if (value != _currentSelectedChartOfAccount)
                {
                    _currentSelectedChartOfAccount = value;
                    OnPropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int COASelectedIndex { get; set; }
        public object AttSelectedItem
        {
            get { return _currentSelectedAttribute; }
            set
            {
                if (value != _currentSelectedAttribute)
                {
                    _currentSelectedAttribute = value;
                    OnPropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int AttSelectedIndex { get; set; }
        public object EmpSelectedItem
        {
            get { return _currentSelectedEmployee; }
            set
            {
                if (value != _currentSelectedEmployee)
                {
                    _currentSelectedEmployee = value;
                    OnPropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int EmpSelectedIndex { get; set; }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand EditEmployeeCommand { get; set; }
        public DelegateCommand AddEmployeeCommand { get; set; }
        public DelegateCommand DeleteEmployeeCommand { get; set; }

        public DelegateCommand EditCOACommand { get; set; }
        public DelegateCommand AddCOACommand { get; set; }
        public DelegateCommand DeleteCOACommand { get; set; }

        public DelegateCommand NewAttributeCommand { get; set; }
        public DelegateCommand EditAttributeCommand { get; set; }
        public DelegateCommand DeleteAttributeCommand { get; set; }
        public DelegateCommand<AddressWrapper> ValidateAddressCommand { get; set; }

        public InteractionRequest<ItemEditNotification> EditEmployeeRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditChartOfAccountRequest { get; set; }
        public InteractionRequest<ItemEditNotification> EditAttributeRequest { get; set; }
        public InteractionRequest<IConfirmation> SaveChangesConfirmationRequest { get; set; }

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

        private void BindCommands()
        {
            SaveCommand = new DelegateCommand(DoSave, CanSave);
            CancelCommand = new DelegateCommand(DoCancel, CanDoCancel);

            EditEmployeeCommand = new DelegateCommand(EditEmployee, CanEditEmployee);
            DeleteEmployeeCommand = new DelegateCommand(DeleteEmployee, CanDeleteEmployee);
            AddEmployeeCommand = new DelegateCommand(AddEmployee);

            EditCOACommand = new DelegateCommand(EditChartOfAccount, CanEditChartOfAccount);
            DeleteCOACommand = new DelegateCommand(DeleteChartOfAccount, CanDeleteChartOfAccount);
            AddCOACommand = new DelegateCommand(AddChartOfAccount);

            NewAttributeCommand = new DelegateCommand(AddAttribute);
            EditAttributeCommand = new DelegateCommand(EditAttribute, CanEditAttribute);
            DeleteAttributeCommand = new DelegateCommand(DeleteAttribute, CanDeleteAttribute);
            
            EditEmployeeRequest = new InteractionRequest<ItemEditNotification>();
            EditChartOfAccountRequest = new InteractionRequest<ItemEditNotification>();
            EditAttributeRequest = new InteractionRequest<ItemEditNotification>();
            SaveChangesConfirmationRequest = new InteractionRequest<IConfirmation>();
            ValidateAddressCommand = new DelegateCommand<AddressWrapper>(ValidateAddress);
            InvalidateCommands();
        }

        private void RegisterApplicationCommands()
        {
            ApplicationCommands.CompanySaveCommand.RegisterCommand(SaveCommand);
            ApplicationCommands.CompanyCancelCommand.RegisterCommand(CancelCommand);

            ApplicationCommands.CompanyEditEmployeeCommand.RegisterCommand(EditEmployeeCommand);
            ApplicationCommands.CompanyAddEmployeeCommand.RegisterCommand(AddEmployeeCommand);
            ApplicationCommands.CompanyDeleteEmployeeCommand.RegisterCommand(DeleteEmployeeCommand);
            //ApplicationCommands.CompanyFindEmployeeCommand.RegisterCommand(DeleteEmployeeCommand);

            ApplicationCommands.CompanyEditCOACommand.RegisterCommand(EditCOACommand);
            ApplicationCommands.CompanyAddCOACommand.RegisterCommand(AddCOACommand);
            ApplicationCommands.CompanyDeleteCOACommand.RegisterCommand(DeleteCOACommand);

            ApplicationCommands.CompanyEditAttributeCommand.RegisterCommand(EditAttributeCommand);
            ApplicationCommands.CompanyAddAttributeCommand.RegisterCommand(NewAttributeCommand);
            ApplicationCommands.CompanyDeleteAttributeCommand.RegisterCommand(DeleteAttributeCommand);
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ApplicationCommands.CompanySaveCommand.UnregisterCommand(SaveCommand);

            ApplicationCommands.CompanyEditEmployeeCommand.UnregisterCommand(EditEmployeeCommand);
            ApplicationCommands.CompanyAddEmployeeCommand.UnregisterCommand(AddEmployeeCommand);
            ApplicationCommands.CompanyDeleteEmployeeCommand.UnregisterCommand(DeleteEmployeeCommand);
            //ApplicationCommands.CompanyFindEmployeeCommand.RegisterCommand(DeleteEmployeeCommand);

            ApplicationCommands.CompanyEditCOACommand.UnregisterCommand(EditCOACommand);
            ApplicationCommands.CompanyAddCOACommand.UnregisterCommand(AddCOACommand);
            ApplicationCommands.CompanyDeleteCOACommand.UnregisterCommand(DeleteCOACommand);

            ApplicationCommands.CompanyEditAttributeCommand.UnregisterCommand(EditAttributeCommand);
            ApplicationCommands.CompanyAddAttributeCommand.UnregisterCommand(NewAttributeCommand);
            ApplicationCommands.CompanyDeleteAttributeCommand.UnregisterCommand(DeleteAttributeCommand);
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            DeleteEmployeeCommand.RaiseCanExecuteChanged();
            EditEmployeeCommand.RaiseCanExecuteChanged();
            EditCOACommand.RaiseCanExecuteChanged();
            DeleteCOACommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            EditAttributeCommand.RaiseCanExecuteChanged();
            DeleteAttributeCommand.RaiseCanExecuteChanged();
        }

        private bool CanDeleteAttribute()
        {
            return AttSelectedItem != null;
        }

        private void DeleteAttribute()
        {
            var att_to_remove = AttSelectedItem as EntityAttributeWrapper;
            if (att_to_remove != null) att_to_remove.AttributeValue = ""; //Company.CompanyAttributes.Remove(att_to_remove);
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
                notification.Title = action + " Attribute";
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

        private bool CanDeleteChartOfAccount()
        {
            return COASelectedItem != null;
        }

        private void DeleteChartOfAccount()
        {
            var coa_to_remove = COASelectedItem as ChartOfAccountWrapper;
            if (coa_to_remove != null)
                Company.GLAccounts.Remove(coa_to_remove);
        }

        private bool CanEditChartOfAccount()
        {
            return COASelectedItem != null;
        }

        private void EditChartOfAccount()
        {
            var coa_to_edit = COASelectedItem as ChartOfAccountWrapper;
            if (coa_to_edit != null)
            {
                ChartOfAccount coa_copy = coa_to_edit.Model.Copy();
                ChangeChartOfAccount(coa_copy, ApplicationStrings.NotificationEdit);
            }
        }

        private void AddChartOfAccount()
        {
            ChartOfAccount coa_to_add = new ChartOfAccount()
            {
                CompanyKey = CurrentCompanyKey
            };
            ChangeChartOfAccount(coa_to_add, ApplicationStrings.NotificationAdd);
        }

        private void ChangeChartOfAccount(ChartOfAccount coa, string action)
        {
            var coa_to_edit = coa as ChartOfAccount;
            if (coa_to_edit != null)
            {
                ItemEditNotification notification = new ItemEditNotification(coa_to_edit);
                notification.Title = action + " Chart Of Account";
                EditChartOfAccountRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            ChartOfAccount coa_changed = r.EditibleObject as ChartOfAccount;
                            if (coa_changed != null)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var coa_to_change = COASelectedItem as ChartOfAccountWrapper;
                                    if (coa_to_change != null)
                                    {
                                        coa_to_change.BalanceType = coa_changed.BalanceType;
                                        coa_to_change.BankAccountFlag = coa_changed.BankAccountFlag;
                                        coa_to_change.AccountName = coa_changed.AccountName;
                                        coa_to_change.AccountNo = coa_changed.AccountNo;
                                        coa_to_change.AccountType = coa_changed.AccountType;
                                    }
                                }
                                else
                                {
                                    Company.GLAccounts.Add(new ChartOfAccountWrapper(coa_changed));
                                }
                            }
                        }
                    });
            }
        }

        private bool CanSave()
        {
            return Company.IsChanged && Company.IsValid;
        }

        private bool CanEditEmployee()
        {
            return EmpSelectedItem != null;
        }

        private void EditEmployee()
        {
            var emp_to_edit = EmpSelectedItem as EmployeeWrapper;
            if (emp_to_edit != null)
            {
                ChangeEmployee(emp_to_edit.Model.Copy(), ApplicationStrings.NotificationEdit);
            }
        }

        private void AddEmployee()
        {
            ChangeEmployee(new Employee(), ApplicationStrings.NotificationAdd);
        }

        private void ChangeEmployee(Employee employee, string action)
        {
            var emp_to_edit = employee as Employee;
            if (emp_to_edit != null)
            {
                var supervisors = Company.Employees.Where(emp => emp.RoleInCompany == Roles.QIQORoleOwner || emp.RoleInCompany == Roles.QIQORoleManager).ToList();
                Tuple<object, object> needed_objects = new Tuple<object, object>(emp_to_edit, supervisors);
                ItemEditNotification notification = new ItemEditNotification(needed_objects);
                //ItemEditNotification notification = new ItemEditNotification(emp_to_edit);
                notification.Title = action + " Employee"; //+ emp_to_edit.PersonCode + " - " + emp_to_edit.PersonFullNameFML;
                EditEmployeeRequest.Raise(notification,
                    r =>
                    {
                        if (r != null && r.Confirmed && r.EditibleObject != null) // 
                        {
                            Employee emp = r.EditibleObject as Employee;
                            if (emp != null)
                            {
                                if (action == ApplicationStrings.NotificationEdit)
                                {
                                    var emp_to_change = EmpSelectedItem as EmployeeWrapper;
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
                                    Company.Employees.Add(new EmployeeWrapper(emp));
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
            var emp_to_remove = EmpSelectedItem as EmployeeWrapper;
            if (emp_to_remove != null)
            {
                if (emp_to_remove.PersonKey != 0)
                {
                    emp_to_remove.EndDate = DateTime.Today;
                }
                else
                    Company.Employees.Remove(emp_to_remove);
            }
        }

        private bool CanDoCancel()
        {
            return Company.IsChanged && Company.IsValid;
        }

        private void DoCancel()
        {
            // this solution is not really working that well, rethink
            _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.DashboardRibbonView);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OpenOrderView);
            //if (Company.IsChanged && !string.IsNullOrWhiteSpace(Company.CompanyCode))
            //{
            //    Confirmation confirm = new Confirmation();
            //    confirm.Title = "Save Changes to Company?";
            //    confirm.Content = "The company has changes. Click OK to save the changes. Click Cancel to leave the Company form.";
            //    SaveChangesConfirmationRequest.Raise(confirm,
            //        r => {
            //            if (r != null && r.Confirmed)
            //            {
            //                DoSave();
            //            }
            //            _regionManager.RequestNavigate(RegionNames.RibbonRegion, ViewNames.DashboardRibbonView);
            //            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.OpenOrderView);
            //        });
            //}
        }

        private void DoSave()
        {
            ExecuteFaultHandledOperation(() =>
            {
                ICompanyService company_service = service_factory.CreateClient<ICompanyService>();
                using (TransactionScope scope = new TransactionScope()) // TransactionScopeAsyncFlowOption.Enabled
                {
                    using (company_service)
                    {
                        MapPropsToObject();
                        int company_key = company_service.CreateCompany(Company.Model);
                        Company.AcceptChanges();
                        event_aggregator.GetEvent<CompanySavedEvent>().Publish("Company saved sucessfully");
                    }
                    scope.Complete();
                }
            });

        }

        private async void GetAttributeList()
        {
            ITypeService type_service = service_factory.CreateClient<ITypeService>();

            using (type_service)
            {
                try
                {
                    Task<List<AttributeType>> task = type_service.GetAttributeTypeListAsync();
                    await task;
                    List<AttributeType> atttype_list = task.Result;
                    ObservableCollection<AttributeType> atttypes = new ObservableCollection<AttributeType>(atttype_list);
                    var acct_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Company").ToList();
                    var gcnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Company Contact").ToList();

                    var all_atts = acct_atts.Concat(gcnt_atts);
                    List< AttributeType> available_attribute_types = new List<AttributeType>(all_atts);
                    LoadEntityAttrbuteList(available_attribute_types);
                }
                catch (Exception ex)
                {
                    DisplayErrorMessage(ex);
                    return;
                }
            }
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
                            EntityKey = CurrentCompanyKey,
                            EntityType = QIQOEntityType.Company,
                            AttributeTypeData = attype,
                            AttributeDataType = attype.AttributeDataTypeKey,
                            EntityTypeData = new EntityType()
                        };

                        var att = Company.CompanyAttributes.Where(item => item.AttributeType == ent_att.AttributeType).ToList();

                        if (att.Count == 0)
                            Company.CompanyAttributes.Add(new EntityAttributeWrapper(ent_att));
                    }
                }
            }
        }

        private async void GetEmployeeList()
        {
            if (_currentCoObject.Employees.Count > 0)
                return;

            IEmployeeService emp_service = service_factory.CreateClient<IEmployeeService>();
            using (emp_service)
            {
                try
                {
                    Task<List<Employee>> task = emp_service.GetEmployeesAsync((Client.Entities.Company)CurrentCompany);
                    await task;
                    List<Employee> emp_list = task.Result;

                    if (emp_list.Count > 0)
                    {
                        Company.Employees.Clear();

                        foreach (Employee emp in emp_list)
                            Company.Employees.Add(new EmployeeWrapper(emp));
                    }
                    Company.AcceptChanges();

                }
                catch (Exception ex)
                {
                    DisplayErrorMessage(ex);
                    return;
                }
            }
        }

        private void MapObjectToProps(Client.Entities.Company company)
        {
            Company = new CompanyWrapper(company);

            if (company.GLAccounts != null)
            {
                AccountTypes = new List<string>(Company.GLAccounts.Select(x => x.AccountType).Distinct().OrderBy(x => x).ToList());
                BalanceTypes = new List<string>(Company.GLAccounts.Select(x => x.BalanceType).Distinct().OrderBy(x => x).ToList());
            }

            if (company.CompanyAddresses != null)
            {
                DefaultShippingAddress = Company.CompanyAddresses.Where(type => type.AddressType == QIQOAddressType.Shipping).FirstOrDefault();
                DefaultBillingAddress = Company.CompanyAddresses.Where(type => type.AddressType == QIQOAddressType.Billing).FirstOrDefault();
            }
            Company.PropertyChanged += Context_PropertyChanged;
        }

        private void GetStateList()
        {
            States = new ObservableCollection<AddressPostal>(address_postal_service.StateList);
        }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }

        private void MapPropsToObject()
        {
            ICleaningUtility cleaner = Unity.Container.Resolve<ICleaningUtility>();
            // Need to add the addresses one at a time from properties??? Ah, yeah!
            Company.CompanyAddresses.Clear(); // clear this for updates and inserts

            cleaner.CleanAddress(DefaultBillingAddress.Model);
            DefaultBillingAddress.AddressType = QIQOAddressType.Billing;
            cleaner.CleanAddress(DefaultShippingAddress.Model);
            DefaultShippingAddress.AddressType = QIQOAddressType.Shipping;
            
            Company.CompanyAddresses.Add(DefaultBillingAddress);
            Company.CompanyAddresses.Add(DefaultShippingAddress);
            Company.GLAccounts.Select(item => item.CompanyKey = CurrentCompanyKey);

            List<EntityAttributeWrapper> atts_with_values = Company.CompanyAttributes.Where(i => i.AttributeValue != "" || i.AttributeKey > 0).ToList();
            Company.CompanyAttributes.Clear();
            foreach (var good_att in atts_with_values)
                Company.CompanyAttributes.Add(good_att);
        }
    }
}
