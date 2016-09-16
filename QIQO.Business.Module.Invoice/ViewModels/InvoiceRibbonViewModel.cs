using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceRibbonViewModel : ViewModelBase, IRegionMemberLifetime
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator event_aggregator;

        public InvoiceRibbonViewModel()
        {
            event_aggregator = Unity.Container.Resolve<IEventAggregator>();
            _regionManager = Unity.Container.Resolve<IRegionManager>();
        }

        public bool KeepAlive { get; } = false;
    }
}
