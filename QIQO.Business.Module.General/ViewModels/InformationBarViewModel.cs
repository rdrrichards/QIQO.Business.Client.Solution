using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using Prism.Events;

namespace QIQO.Business.Module.General.ViewModels
{
    public class InformationBarViewModel : ViewModelBase
    {
        IEventAggregator event_aggregator;

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
