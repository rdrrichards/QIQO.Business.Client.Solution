using CommonServiceLocator;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Company.ViewModels
{
    public class CompanyRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public CompanyRibbonViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            SomeCommand = new DelegateCommand<string>(DoSomething);
        }

        public bool KeepAlive { get; } = false;

        public DelegateCommand<string> SomeCommand { get; set; }

        private void DoSomething(string message)
        {
            event_aggregator.GetEvent<GeneralMessageEvent>().Publish(message);
        }
    }
}
