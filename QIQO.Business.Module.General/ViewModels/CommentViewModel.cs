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
    class CommentViewModel : ViewModelBase, IInteractionRequestAware
    {
        IEventAggregator event_aggregator;
        IServiceFactory service_factory;
        private CommentWrapper _contact;

        private ItemEditNotification notification;

        public CommentViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            service_factory = Unity.Container.Resolve<IServiceFactory>();

            CurrentComment = new CommentWrapper(new Comment());
            BindCommands();
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CommentTypeChangedCommand { get; set; }

        public CommentWrapper CurrentComment
        {
            get { return _contact; }
            private set { SetProperty(ref _contact, value); }
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
                    var contact = notification.EditibleObject as Comment;
                    if (contact != null)
                    {
                        CurrentComment = new CommentWrapper(contact); // need to confirm this is enough to isolate the passed in object 
                        CurrentComment.PropertyChanged += Context_PropertyChanged;
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
            CommentTypeChangedCommand = new DelegateCommand(OnCommentTypeChanged);
        }

        private void OnCommentTypeChanged()
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
            CurrentComment = null;
        }

        private bool CanDoSave()
        {
            if (CurrentComment.CommentKey == 0)
                return !HasErrors;
            else
                return CurrentComment.IsChanged && CurrentComment.IsValid;
        }

        private void DoSave()
        {
            notification.EditibleObject = CurrentComment.Model;
            notification.Confirmed = true;
            FinishInteraction();
        }
    }
}
