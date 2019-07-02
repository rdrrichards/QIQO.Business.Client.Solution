using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Invoices.Views;

namespace QIQO.Business.Module.Invoices.Modules
{
    public class InvoiceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(InvoiceNavigationView));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(object), typeof(InvoiceShellView), typeof(InvoiceShellView).FullName);
            containerRegistry.Register(typeof(object), typeof(InvoiceView), typeof(InvoiceView).FullName);
            containerRegistry.Register(typeof(object), typeof(FindInvoiceView), typeof(FindInvoiceView).FullName);
            containerRegistry.Register(typeof(object), typeof(InvoiceRibbonView), typeof(InvoiceRibbonView).FullName);
        }
    }
}
