using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Entities;
using QIQO.Business.Client.Wrappers;
using System;
using System.ComponentModel;

namespace QIQO.Business.Module.General.ViewModels
{
    public class AttributeViewModel : ViewModelBase, IInteractionRequestAware
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        EntityAttributeWrapper _entity_attribute;
        private string _viewTitle = "Attribute Add/Edit";

        private ItemEditNotification notification;

        public AttributeViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>(); 
            service_factory = ServiceLocator.Current.GetInstance<IServiceFactory>();

            EntityAttribute = new EntityAttributeWrapper(new EntityAttribute());
            BindCommands();
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public override string ViewTitle { get { return _viewTitle; } }

        public Action FinishInteraction { get; set; }

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
                    var entity_attribute = notification.EditibleObject as EntityAttribute;
                    if (entity_attribute != null)
                    {
                        EntityAttribute = new EntityAttributeWrapper(entity_attribute); // need to confirm this is enough to isolate the passed in object 
                        EntityAttribute.PropertyChanged += Context_PropertyChanged;
                    }
                    RaisePropertyChanged(nameof(Notification));
                }
            }
        }

        public EntityAttributeWrapper EntityAttribute
        {
            get { return _entity_attribute; }
            private set { SetProperty(ref _entity_attribute, value); }
        }

        private void BindCommands()
        {
            CancelCommand = new DelegateCommand(DoCancel);
            SaveCommand = new DelegateCommand(DoSave, CanDoSave);
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            InvalidateCommands();
        }

        private void InvalidateCommands()
        {
            CancelCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void DoCancel()
        {
            EntityAttribute = null;
            // Close window
        }

        private bool CanDoSave()
        {
            //**** NEEDS WORK!
            if (EntityAttribute.AttributeKey == 0)
                return !HasErrors;
            else
                return EntityAttribute.IsChanged && EntityAttribute.IsValid;
            //return true;

        }

        private void DoSave()
        {
            notification.EditibleObject = EntityAttribute.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
