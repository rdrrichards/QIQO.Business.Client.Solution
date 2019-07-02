using Prism.Events;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using System;
using System.Threading;

namespace QIQO.Business.Module.General.ViewModels
{
    public class StatusBarViewModel : ViewModelBase
    {
        readonly IEventAggregator event_aggregator;
        string _StatusText1;

        public StatusBarViewModel(IEventAggregator event_aggtr)
        {
            event_aggregator = event_aggtr;
            StatusText1 = MessageToDisplay;

            event_aggregator.GetEvent<AccountUpdatedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<CompanyLoadedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<GeneralErrorEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<CompanySavedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<GeneralMessageEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<OrderUpdatedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<AccountDeletedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<OrderDeletedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            //event_aggregator.GetEvent<OrderLoadedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);

            event_aggregator.GetEvent<InvoiceUpdatedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            event_aggregator.GetEvent<InvoiceDeletedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
            //event_aggregator.GetEvent<InvoiceLoadedEvent>().Subscribe(OnUpdateStatus, ThreadOption.BackgroundThread);
        }

        public string StatusText1
        {
            get { return _StatusText1; }
            private set { SetProperty(ref _StatusText1, value); }
        }

        public string CurrentDate { get { return DateTime.Today.ToString("yyyy-MM-dd"); } }

        private void OnUpdateStatus(string obj)
        {
            StatusText1 = obj;
            Thread.Sleep(5000);
            StatusText1 = MessageToDisplay;
        }
    }
}
