using CommonServiceLocator;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public InvoiceRibbonViewModel()
        {
            event_aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }

        public bool KeepAlive { get; } = false;
    }
}
