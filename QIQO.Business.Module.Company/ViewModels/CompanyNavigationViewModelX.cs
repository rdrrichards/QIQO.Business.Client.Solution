using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;

namespace QIQO.Business.Module.Company.ViewModels
{
    public class CompanyNavigationViewModelX : NavigationViewModelBase
    {
        public CompanyNavigationViewModelX(IEventAggregator evnt_aggr, IRegionManager rm) : base (evnt_aggr, rm)
        {
            Module = ViewNames.CompanyView;
        }
    }
}
