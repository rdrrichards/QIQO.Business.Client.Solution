using CommonServiceLocator;
using Prism.Logging;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using QIQO.Business.Client.Contracts;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Client.Core.UI.Adapters;
using QIQO.Business.Client.Proxies;
using QIQO.Business.Client.UI.Views;
using QIQO.Business.Module.Account.Modules;
using QIQO.Business.Module.Company.Modules;
using QIQO.Business.Module.Dashboard.Modules;
using QIQO.Business.Module.General.Modules;
using QIQO.Business.Module.Invoices.Modules;
using QIQO.Business.Module.Orders.Modules; //QIQO.Business.Module.Orders.Modules
using QIQO.Business.Module.Product.Modules;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Client.UI
{
    public class Bootstrapper : UnityBootstrapper
    {
        // The following run basically in this order
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            //catalog.AddModule(typeof(CalendarBarModule));
            catalog.AddModule(typeof(StatusBarModule));
            catalog.AddModule(typeof(DashboardModule));

            catalog.AddModule(typeof(AccountModule));
            catalog.AddModule(typeof(OrderModule));
            catalog.AddModule(typeof(InvoiceModule));
            catalog.AddModule(typeof(ProductModule));
            catalog.AddModule(typeof(CompanyModule));

            catalog.AddModule(typeof(OpenOrderModule));
            catalog.AddModule(typeof(OpenInvoiceModule));

            catalog.AddModule(typeof(CalendarBarModule));

            return catalog;
        }

        protected override void ConfigureContainer()
        {
            RegisterTypeIfMissing(typeof(IModuleInitializer), typeof(QIQOModuleInitializer), true);

            base.ConfigureContainer();

            InitContainer();

            // Unity.Container = Container;
        }

        private void InitContainer()
        {
            RegisterTypeIfMissing(typeof(IShellViewModel), typeof(ShellViewModel), true);
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

            var company = ServiceLocator.Current.GetInstance<ICurrentCompanyService>();

            if (!company.IsMultiCompanyEmployee)
            {
                Application.Current.MainWindow.Show();
            }
            else
            {
                if (company.CompanyPromptOnLoad)
                {
                    var company_chooser = new CompanyChooserViewModel(company.EmployeeCompanies);
                    var company_chooser_view = new CompanyChooserView(company_chooser);
                    company_chooser_view.Show();
                }
            }
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(StackPanel), ServiceLocator.Current.GetInstance<StackPanelRegionAdapter>());
            mappings.RegisterMapping(typeof(Ribbon), ServiceLocator.Current.GetInstance<RibbonRegionAdapter>());
            return mappings;
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new LoggerAdapter();
        }
    }
}
