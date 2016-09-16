using System;
using Microsoft.Practices.Unity;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Module.Invoices.Views;
using QIQO.Business.Client.Core.Infrastructure;

namespace QIQO.Business.Module.Invoices.Modules
{
    public class InvoiceModule : ModuleBase
    {
        public InvoiceModule(IUnityContainer container, IRegionManager region_manager) : base(container, region_manager)
        {

        }

        public override void Initialize()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(InvoiceNavigationView));

            UnityContainer.RegisterType(typeof(object), typeof(InvoiceShellView), typeof(InvoiceShellView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(InvoiceView), typeof(InvoiceView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(FindInvoiceView), typeof(FindInvoiceView).FullName);
            UnityContainer.RegisterType(typeof(object), typeof(InvoiceRibbonView), typeof(InvoiceRibbonView).FullName);
        }
    }
}
