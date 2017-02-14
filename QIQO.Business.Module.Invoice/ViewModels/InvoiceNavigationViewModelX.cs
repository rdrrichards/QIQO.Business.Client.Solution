using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceNavigationViewModelX : NavigationViewModelBase
    {
        public InvoiceNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.InvoiceHomeView;
        }
    }
}
