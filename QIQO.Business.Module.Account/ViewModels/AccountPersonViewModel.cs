using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QIQO.Business.Module.Account.ViewModels
{
    public class AccountPersonViewModel : ViewModelBase, IInteractionRequestAware
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private AccountPersonWrapper _person;

        private ItemEditNotification notification;
        public AccountPersonViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            service_factory = ServiceLocator.Current.GetInstance<IServiceFactory>();

            CurrentPerson = new AccountPersonWrapper(new AccountPerson());
            BindCommands();
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand RoleChangedCommand { get; set; }
        public DelegateCommand GenPersonCodeCommand { get; set; }

        public AccountPersonWrapper CurrentPerson
        {
            get { return _person; }
            private set { SetProperty(ref _person, value); }
        }

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
                    var person = notification.EditibleObject as AccountPerson;
                    if (person != null)
                    {
                        CurrentPerson = new AccountPersonWrapper(person); // need to confirm this is enough to isolate the passed in object 
                        CurrentPerson.PropertyChanged += Context_PropertyChanged;
                        InvalidateCommands();
                    }
                    RaisePropertyChanged(nameof(Notification));
                }
            }
        }

        public Action FinishInteraction { get; set; }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel);
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            RoleChangedCommand = new DelegateCommand(OnRoleTypeChanged);
            GenPersonCodeCommand = new DelegateCommand(GenPersonCode, CanGenPersonCode);
        }

        private bool CanGenPersonCode()
        {
            if (CurrentPerson != null)
                return ((CurrentPerson.PersonCode == "" || CurrentPerson.PersonCode == null) && 
                    ((CurrentPerson.PersonFirstName != null && CurrentPerson.PersonFirstName.Length > 0) & 
                    (CurrentPerson.PersonLastName != null && CurrentPerson.PersonLastName.Length > 0)));
            return false;
        }

        private void GenPersonCode()
        {
            // TODO: add code to go get an employee code that doesn't already exist!
            //var account_service = service_factory.CreateClient<IAccountService>();
            //CurrentPerson.PersonCode = account_service.GetAccountNextNumber(CurrentPerson.Account.Model, QIQOEntityNumberType.OrderNumber);
            //account_service.Dispose();
            CurrentPerson.PersonCode = CurrentPerson.PersonFirstName[0] + ((CurrentPerson.PersonMI != null && 
                CurrentPerson.PersonMI.Length > 0) ? CurrentPerson.PersonMI : string.Empty) + CurrentPerson.PersonLastName[0];
        }

        private void OnRoleTypeChanged()
        {
            //throw new NotImplementedException();
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            SaveCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
            RoleChangedCommand.RaiseCanExecuteChanged();
            GenPersonCodeCommand.RaiseCanExecuteChanged();
        }

        private void DoCancel()
        {
            CurrentPerson = null;
        }

        private bool CanDoSave()
        {
            if (CurrentPerson.PersonKey == 0)
                return !HasErrors;
            else
                return CurrentPerson.IsChanged && CurrentPerson.IsValid;
        }

        private void DoSave()
        {
            CurrentPerson.AcceptChanges();
            notification.EditibleObject = CurrentPerson.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
