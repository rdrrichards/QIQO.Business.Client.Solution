using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Invoices.Views;

namespace QIQO.Business.Module.Invoices.Modules
{
    public class OpenInvoiceModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.DashboardRegion2, typeof(OpenInvoiceViewX));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
