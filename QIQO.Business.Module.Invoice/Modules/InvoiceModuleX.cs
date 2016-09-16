using System;
using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Module.Invoices.Views;
using QIQO.Business.Client.Core.Infrastructure;
using Prism.Unity;
using QIQO.Business.Module.Invoices.Services;

namespace QIQO.Business.Module.Invoices.Modules
{
    public class InvoiceModuleX : ModuleBase
    {
        public InvoiceModuleX(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {

        }

        public override void Initialize()
        {
            UnityContainer.RegisterType<IWorkingInvoiceService, WorkingInvoiceService>(new ContainerControlledLifetimeManager());

            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(InvoiceNavigationViewX));

            RegionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeOpenInvoiceRegion, typeof(OpenInvoiceViewX));
            //RegionManager.RegisterViewWithRegion(RegionNames.OrdersHomeRecentOrderRegion, typeof(WorkingInvoiceView));
            RegionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeRecentInvoiceRegion, typeof(WorkingInvoiceView));
            RegionManager.RegisterViewWithRegion(RegionNames.InvoicesHomeSearchInvoiceRegion, typeof(FindInvoiceViewX));


            UnityContainer.RegisterTypeForNavigation<InvoiceHomeView>(typeof(InvoiceHomeView).FullName);
            UnityContainer.RegisterTypeForNavigation<InvoiceViewX>(typeof(InvoiceViewX).FullName);
        }
    }
}
