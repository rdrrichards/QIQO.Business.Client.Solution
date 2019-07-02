using Prism.Events;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.General.ViewModels
{
    public class InformationBarViewModel : ViewModelBase
    {
        readonly IEventAggregator event_aggregator;

        public InformationBarViewModel(IEventAggregator event_aggtr)
        {
            event_aggregator = event_aggtr;
            event_aggregator.GetEvent<GeneralMessageEvent>().Subscribe(OnUpdateMessage, ThreadOption.BackgroundThread);
            MessageToDisplay = "Shabam!";
            IsMessageVisible = true;
        }

        private void OnUpdateMessage(string obj)
        {
            MessageToDisplay = obj;
        }
    }
}
