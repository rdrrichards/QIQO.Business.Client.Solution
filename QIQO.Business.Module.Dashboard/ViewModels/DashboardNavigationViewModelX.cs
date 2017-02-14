using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.Dashboard.ViewModels
{
    public class DashboardNavigationViewModelX : NavigationViewModelBase
    {
        public DashboardNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.DashboardViewX;
        }
    }
}
