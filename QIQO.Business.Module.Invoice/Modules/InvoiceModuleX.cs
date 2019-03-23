using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using QIQO.Business.Module.Invoices.Views;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Module.Invoices.Services;

namespace QIQO.Business.Module.Invoices.Modules
{
    public class InvoiceModuleX : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(InvoiceNavigationViewX));
            regionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeOpenInvoiceRegion, typeof(OpenInvoiceViewX));
            regionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeRecentInvoiceRegion, typeof(WorkingInvoiceView));
            regionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeSearchInvoiceRegion, typeof(FindInvoiceViewX));
        }
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IWorkingInvoiceService, WorkingInvoiceService>();

            //containerRegistry.Register(typeof(object), typeof(InvoiceShellView), typeof(InvoiceShellView).FullName);
            //containerRegistry.Register(typeof(object), typeof(InvoiceView), typeof(InvoiceView).FullName);
            //containerRegistry.Register(typeof(object), typeof(FindInvoiceView), typeof(FindInvoiceView).FullName);
            //containerRegistry.Register(typeof(object), typeof(InvoiceRibbonView), typeof(InvoiceRibbonView).FullName);

            containerRegistry.RegisterForNavigation<InvoiceHomeView>(typeof(InvoiceHomeView).FullName);
            containerRegistry.RegisterForNavigation<InvoiceViewX>(typeof(InvoiceViewX).FullName);


        }
    }
}
