using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using CommonServiceLocator;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QIQO.Business.Client.Contracts;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.General.ViewModels
{
    public class EmployeeViewModel : ViewModelBase, IInteractionRequestAware
    {
        private readonly IEventAggregator event_aggregator;
        private readonly IServiceFactory service_factory;
        private readonly IStateListService address_postal_service;
        private EmployeeWrapper _currentEmployee;
        private AddressWrapper _default_address;
        private ItemEditNotification notification;
        private ObservableCollection<AddressPostal> _states;
        private string _viewTitle = "Employee Add/Edit";
        private object _currentSelectedAttribute;
        private List<EmployeeWrapper> _supervisors;

        public EmployeeViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            service_factory = ServiceLocator.Current.GetInstance<IServiceFactory>();
            address_postal_service = ServiceLocator.Current.GetInstance<IStateListService>();

            BindCommands();
            GetStateList();
        }

        public EmployeeWrapper CurrentEmployee
        {
            get { return _currentEmployee; }
            private set { SetProperty(ref _currentEmployee, value); }
        }

        public List<EmployeeWrapper> Supervisors
        {
            get { return _supervisors; }
            private set { SetProperty(ref _supervisors, value); }
        }

        public AddressWrapper DefaultAddress
        {
            get { return _default_address; }
            private set { SetProperty(ref _default_address, value); InvalidateCommands(); }
        }

        public List<string> CompanyRoles // Get this centralized
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
        public override string ViewTitle { get { return _viewTitle; } }

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand EditAttributeCommand { get; set; }
        public DelegateCommand DeleteAttributeCommand { get; set; }
        public DelegateCommand ValidateAddressCommand { get; set; }
        public DelegateCommand GenEmployeeCodeCommand { get; set; }
        public InteractionRequest<ItemEditNotification> EditAttributeRequest { get; set; }

        public INotification Notification
        {
            get
            {
                return notification;
            }
            set
            {
                if (value is ItemEditNotification)
                {
                    notification = value as ItemEditNotification;
                    var objects = notification.EditibleObject as Tuple<object, object>;
                    if (objects != null)
                    {
                        var employee = objects.Item1 as Employee;
                        if (employee != null && employee.EntityPersonKey > 0)
                            GetEmployeeToEdit(employee);
                        else
                            WrapEmployee(employee);
                        
                        var supers = objects.Item2 as List<EmployeeWrapper>;
                        if (supers != null)
                        {
                            Supervisors = supers;
                        }

                        RaisePropertyChanged(nameof(Notification));
                    }
                }
            }
        }

        public Action FinishInteraction { get; set; }
        public object AttSelectedItem
        {
            get { return _currentSelectedAttribute; }
            set
            {
                if (value != _currentSelectedAttribute)
                {
                    _currentSelectedAttribute = value;
                    RaisePropertyChanged();
                    InvalidateCommands();
                }
            }
        }
        public int AttSelectedIndex { get; set; }

        protected override void DisplayErrorMessage(Exception ex, [CallerMemberName] string methodName = "")
        {
            event_aggregator.GetEvent<GeneralErrorEvent>().Publish(methodName + " - " + ex.Message);
        }

        private void BindCommands()
        {
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            EditAttributeCommand = new DelegateCommand(EditAttribute, CanEditAttribute);
            DeleteAttributeCommand = new DelegateCommand(DeleteAttribute, CanDeleteAttribute);
            ValidateAddressCommand = new DelegateCommand(ValidateAddress);
            GenEmployeeCodeCommand = new DelegateCommand(GenEmployeeCode, CanGenEmployeeCode);
            EditAttributeRequest = new InteractionRequest<ItemEditNotification>();
        }

        private bool CanGenEmployeeCode()
        {
            if (CurrentEmployee != null)
                return (CurrentEmployee.PersonCode == "" || CurrentEmployee.PersonCode == null);
            return false;
        }

        private void GenEmployeeCode()
        {
            // TODO: add code to go get an employee code that doesn't already exist!
            var account_service = service_factory.CreateClient<ICompanyService>();
            CurrentEmployee.PersonCode = account_service.GetCompanyNextNumber((Company)CurrentCompany, QIQOEntityNumberType.EmployeeNumber);
            account_service.Dispose();
        }

        private bool CanDoSave()
        {
            if (CurrentEmployee == null) return false;
            return ((CurrentEmployee.IsChanged && CurrentEmployee.IsValid) || (DefaultAddress.IsChanged && DefaultAddress.IsValid));
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            EditAttributeCommand.RaiseCanExecuteChanged();
            DeleteAttributeCommand.RaiseCanExecuteChanged();
            GenEmployeeCodeCommand.RaiseCanExecuteChanged();
        }

        private void DoSave()
        {
            ExecuteFaultHandledOperation(() =>
            {
                IEmployeeService employee_service = service_factory.CreateClient<IEmployeeService>();
                ICleaningUtility cleaner = ServiceLocator.Current.GetInstance<ICleaningUtility>();

                cleaner.CleanAddress(DefaultAddress.Model);
                DefaultAddress.AddressType = QIQOAddressType.Mailing;
                CurrentEmployee.Model.Addresses.Add(DefaultAddress.Model);

                //foreach (var att in CurrentEmployee.PersonAttributes)
                //    if (att.AttributeValue == "") CurrentEmployee.PersonAttributes.Remove(att);

                using (employee_service)
                {
                    int emp_key = employee_service.CreateEmployee(CurrentEmployee.Model);
                    notification.Confirmed = true;
                    FinishInteraction();
                }
            });
        }

        private void GetEmployeeToEdit(Employee employee)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IEmployeeService employee_service = service_factory.CreateClient<IEmployeeService>();
                using (employee_service)
                {
                    Employee emp = employee_service.GetEmployee(employee.EntityPersonKey);

                    if (emp != null)
                    {
                        WrapEmployee(emp);
                    }

                }
            });
        }

        private void WrapEmployee(Employee emp)
        {
            if (emp.EntityPersonKey == 0) GetNewEmployeeAttributes(emp);
            CurrentEmployee = new EmployeeWrapper(emp);
            CurrentEmployee.PropertyChanged += Context_PropertyChanged;
            if (emp.Addresses.Count > 0)
                DefaultAddress = new AddressWrapper(emp.Addresses[0]);
            else
                DefaultAddress = new AddressWrapper(new Address());
        }

        private void GetNewEmployeeAttributes(Employee employee)
        {
            ExecuteFaultHandledOperation(() =>
            {
                ITypeService type_service = service_factory.CreateClient<ITypeService>();

                using (type_service)
                {
                    try
                    {
                        List<AttributeType> atttype_list = type_service.GetAttributeTypeList();
                        var acct_atts = atttype_list.Where(item => item.AttributeTypeCategory == "Employee").ToList();
                        var gcnt_atts = atttype_list.Where(item => item.AttributeTypeCategory == "General Contact").ToList();

                        var all_atts = acct_atts.Concat(gcnt_atts);
                        foreach (var att in all_atts)
                            employee.PersonAttributes.Add(new EntityAttribute()
                            {
                                AttributeDataTypeKey = (int)att.AttributeDataTypeKey,
                                AttributeDisplayFormat = att.AttributeDefaultFormat,
                                AttributeKey = 0,
                                AttributeType = (QIQOAttributeType)att.AttributeTypeKey,
                                AttributeValue = "",
                                EntityKey = employee.EntityPersonKey,
                                EntityType = QIQOEntityType.Person,
                                AttributeTypeData = att,
                                AttributeDataType = att.AttributeDataTypeKey,
                                EntityTypeData = new EntityType()
                            });
                    }
                    catch (Exception ex)
                    {
                        DisplayErrorMessage(ex);
                        return;
                    }
                }
            });
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

        private void DeleteAttribute()
        {
            var att_to_remove = AttSelectedItem as EntityAttributeWrapper;
            if (att_to_remove != null) att_to_remove.AttributeValue = ""; //Company.CompanyAttributes.Remove(att_to_remove);
        }

        private bool CanDeleteAttribute()
        {
            return AttSelectedItem != null;
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

        private void GetStateList()
        {
            States = new ObservableCollection<AddressPostal>(address_postal_service.StateList);
        }
        public async void ValidateAddress()
        {
            IAddressService addr_service = service_factory.CreateClient<IAddressService>();
            using (addr_service)
            {
                try
                {
                    Task<AddressPostal> task = addr_service.GetAddressInfoByPostalAsync(DefaultAddress.AddressPostalCode);
                    await task;
                    AddressPostal postal_info = task.Result;

                    if (postal_info != null)
                    {
                        DefaultAddress.AddressCity = postal_info.CityName;
                        DefaultAddress.AddressState = postal_info.StateCode;
                        DefaultAddress.AddressCounty = postal_info.CountyName;
                        DefaultAddress.AddressCountry = postal_info.CountryName;
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
    }
}
