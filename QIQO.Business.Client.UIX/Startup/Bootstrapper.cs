using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core.UI.Adapters;
using QIQO.Business.Client.Proxies;
using QIQO.Business.Client.UIX.Views;
using QIQO.Business.Module.Dashboard.Modules;
using QIQO.Business.Module.Account.Modules;
using QIQO.Business.Module.Company.Modules;
using QIQO.Business.Module.General.Modules;
using QIQO.Business.Module.Orders.Modules;
using QIQO.Business.Module.Product.Modules;
using QIQO.Business.Module.Invoices.Modules;
using System.Windows;
using System.Windows.Controls;
using System.Security.Principal;
using System.Threading;

namespace QIQO.Business.Client.UIX
{
    public class Bootstrapper : UnityBootstrapper
    {
        // The following run basically in this order
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            catalog.AddModule(typeof(TitleBarModule));
            //catalog.AddModule(typeof(StatusBarModule));
            catalog.AddModule(typeof(DashboardModuleX));

            catalog.AddModule(typeof(AccountModuleX));
            catalog.AddModule(typeof(OrderModuleX));
            catalog.AddModule(typeof(InvoiceModuleX));
            catalog.AddModule(typeof(ProductModuleX));
            catalog.AddModule(typeof(CompanyModuleX));

            catalog.AddModule(typeof(OpenOrderModuleX));
            catalog.AddModule(typeof(OpenInvoiceModuleX));

            catalog.AddModule(typeof(CalendarBarModuleX));
            catalog.AddModule(typeof(QuickLinkModule));

            return catalog;
        }

        protected override void ConfigureContainer()
        {
            RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(QIQOModuleInitializer), true);

            base.ConfigureContainer();

            InitContainer();

            Unity.Container = Container;
        }

        private void InitContainer()
        {
            //RegisterTypeIfMissing(typeof(IShellViewModel), typeof(Shell), true);
            RegisterTypeIfMissing(typeof(ICurrentCompanyService), typeof(CurrentCompanyService), true);
            RegisterTypeIfMissing(typeof(IProductListService), typeof(ProductListService), true);
            RegisterTypeIfMissing(typeof(IStateListService), typeof(StateListService), true);
            RegisterTypeIfMissing(typeof(IServiceFactory), typeof(ServiceFactory), false);
            RegisterTypeIfMissing(typeof(IAuditService), typeof(AuditClient), false);
            RegisterTypeIfMissing(typeof(ISessionService), typeof(SessionClient), false);
            RegisterTypeIfMissing(typeof(ICompanyService), typeof(CompanyClient), false);
            RegisterTypeIfMissing(typeof(IAccountService), typeof(AccountClient), false);
            RegisterTypeIfMissing(typeof(IAddressService), typeof(AddressClient), false);
            RegisterTypeIfMissing(typeof(IEmployeeService), typeof(EmployeeClient), false);
            RegisterTypeIfMissing(typeof(IProductService), typeof(ProductClient), false);
            RegisterTypeIfMissing(typeof(IEntityProductService), typeof(EntityProductClient), false);
            RegisterTypeIfMissing(typeof(IFeeScheduleService), typeof(FeeScheduleClient), false);
            RegisterTypeIfMissing(typeof(ITypeService), typeof(TypeClient), false);
            RegisterTypeIfMissing(typeof(IOrderService), typeof(OrderClient), false);
            RegisterTypeIfMissing(typeof(IInvoiceService), typeof(InvoiceClient), false);
            RegisterTypeIfMissing(typeof(ILedgerService), typeof(LedgerClient), false);
            RegisterTypeIfMissing(typeof(ICleaningUtility), typeof(CleaningUtility), true);
            RegisterTypeIfMissing(typeof(IReportService), typeof(DefaultReportService), true);
            RegisterTypeIfMissing(typeof(IAccountEntityService), typeof(AccountEntityService), false);
            RegisterTypeIfMissing(typeof(IFeeScheduleEntityService), typeof(FeeScheduleEntityService), true);

        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var ident = WindowsIdentity.GetCurrent();
            var prin = new WindowsPrincipal(ident); //, new string[] { "QIQOCompanyAdmin" });
            Thread.CurrentPrincipal = prin;

            var company = Unity.Container.Resolve<ICurrentCompanyService>();

            if (!company.IsMultiCompanyEmployee)
            {
                Application.Current.MainWindow.Show();
            }
            else
            {
                if (company.CompanyPromptOnLoad)
                {
                    CompanyChooserViewModel company_chooser = new CompanyChooserViewModel(company.EmployeeCompanies);
                    CompanyChooserView company_chooser_view = new CompanyChooserView(company_chooser);
                    company_chooser_view.Show();
                }
            }
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            RegionAdapterMappings mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
            //mappings.RegisterMapping(typeof(Ribbon), Container.Resolve<RibbonRegionAdapter>());
            return mappings;
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerAdapter();
        }
    }
}
