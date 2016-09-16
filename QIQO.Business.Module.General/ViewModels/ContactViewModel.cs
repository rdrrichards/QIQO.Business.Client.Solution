using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace QIQO.Business.Module.General.ViewModels
{
    public class ContactViewModel : ViewModelBase, IInteractionRequestAware
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private ContactWrapper _contact;

        private ItemEditNotification notification;

        public ContactViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();

            CurrentContact = new ContactWrapper(new Contact());
            BindCommands();
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ContactTypeChangedCommand { get; set; }

        public ContactWrapper CurrentContact
        {
            get { return _contact; }
            private set { SetProperty(ref _contact, value); }
        }

        public List<string> ContactTypes // Get this centralized
        {
            get
            {
                return new List<string>(new string[] { "Email", "Phone" });
            }
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
                    var contact = notification.EditibleObject as Contact;
                    if (contact != null)
                    {
                        CurrentContact = new ContactWrapper(contact); // need to confirm this is enough to isolate the passed in object 
                        CurrentContact.PropertyChanged += Context_PropertyChanged;
                    }
                    OnPropertyChanged(() => Notification);
                }
            }
        }

        public Action FinishInteraction { get; set; }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel);
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
            ContactTypeChangedCommand = new DelegateCommand(OnContactTypeChanged);
        }

        private void OnContactTypeChanged()
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
        }

        private void DoCancel()
        {
            CurrentContact = null;
        }

        private bool CanDoSave()
        {
            if (CurrentContact.ContactKey == 0)
                return !HasErrors;
            else
                return CurrentContact.IsChanged && CurrentContact.IsValid;
        }

        private void DoSave()
        {
            notification.EditibleObject = CurrentContact.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
