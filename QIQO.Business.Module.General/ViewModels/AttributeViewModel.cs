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
        readonly IEventAggregator _event_aggregator;
        readonly IServiceFactory _service_factory;
        EntityAttributeWrapper _entity_attribute;
        private readonly string _viewTitle = "Attribute Add/Edit";

        private ItemEditNotification _notification;

        public AttributeViewModel()
        {
            _event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _service_factory = ServiceLocator.Current.GetInstance<IServiceFactory>();

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
                return _notification;
            }
            set
            {
                if (value is ItemEditNotification)
                {
                    _notification = value as ItemEditNotification;
                    if (_notification.EditibleObject is EntityAttribute entity_attribute)
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
            {
                return !HasErrors;
            }
            else
            {
                return EntityAttribute.IsChanged && EntityAttribute.IsValid;
            }
            //return true;

        }

        private void DoSave()
        {
            _notification.EditibleObject = EntityAttribute.Model;
            _notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
