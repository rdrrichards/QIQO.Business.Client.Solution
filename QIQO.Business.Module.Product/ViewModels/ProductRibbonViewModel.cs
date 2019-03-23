using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using CommonServiceLocator;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core;

namespace QIQO.Business.Module.Product.ViewModels
{
    class ProductRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public ProductRibbonViewModel()
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
