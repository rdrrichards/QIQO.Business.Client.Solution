using Prism.Ioc;
using Prism.Modularity;
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
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace QIQO.Business.Client.UIX
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += OnDispatcherUnhandledException;

            base.OnStartup(e);
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // throw new System.NotImplementedException();
            //containerRegistry.RegisterSingleton(typeof(IDataRepositoryFactory), typeof(DataRepositoryFactory));
            //containerRegistry.Register(typeof(IMessageRepository), typeof(MessageRepository));
            containerRegistry.RegisterSingleton(typeof(ICurrentCompanyService), typeof(CurrentCompanyService));
            containerRegistry.RegisterSingleton(typeof(IProductListService), typeof(ProductListService));
            containerRegistry.RegisterSingleton(typeof(IStateListService), typeof(StateListService));
            containerRegistry.Register(typeof(IServiceFactory), typeof(ServiceFactory));
            containerRegistry.Register(typeof(IAuditService), typeof(AuditClient));
            containerRegistry.Register(typeof(ISessionService), typeof(SessionClient));
            containerRegistry.Register(typeof(ICompanyService), typeof(CompanyClient));
            containerRegistry.Register(typeof(IAccountService), typeof(AccountClient));
            containerRegistry.Register(typeof(IAddressService), typeof(AddressClient));
            containerRegistry.Register(typeof(IEmployeeService), typeof(EmployeeClient));
            containerRegistry.Register(typeof(IProductService), typeof(ProductClient));
            containerRegistry.Register(typeof(IEntityProductService), typeof(EntityProductClient));
            containerRegistry.Register(typeof(IFeeScheduleService), typeof(FeeScheduleClient));
            containerRegistry.Register(typeof(ITypeService), typeof(TypeClient));
            containerRegistry.Register(typeof(IOrderService), typeof(OrderClient));
            containerRegistry.Register(typeof(IInvoiceService), typeof(InvoiceClient));
            containerRegistry.Register(typeof(ILedgerService), typeof(LedgerClient));
            containerRegistry.RegisterSingleton(typeof(ICleaningUtility), typeof(CleaningUtility));
            containerRegistry.RegisterSingleton(typeof(IReportService), typeof(DefaultReportService));
            containerRegistry.Register(typeof(IAccountEntityService), typeof(AccountEntityService));
            containerRegistry.RegisterSingleton(typeof(IFeeScheduleEntityService), typeof(FeeScheduleEntityService));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // moduleCatalog.AddModule<ModuleA.ModuleAModule>();
            moduleCatalog.AddModule(typeof(TitleBarModule));
            //catalog.AddModule(typeof(StatusBarModule));
            moduleCatalog.AddModule(typeof(DashboardModuleX));

            moduleCatalog.AddModule(typeof(AccountModuleX));
            moduleCatalog.AddModule(typeof(OrderModuleX));
            moduleCatalog.AddModule(typeof(InvoiceModuleX));
            moduleCatalog.AddModule(typeof(ProductModuleX));
            moduleCatalog.AddModule(typeof(CompanyModuleX));

            moduleCatalog.AddModule(typeof(OpenOrderModuleX));
            moduleCatalog.AddModule(typeof(OpenInvoiceModuleX));

            moduleCatalog.AddModule(typeof(CalendarBarModuleX));
            moduleCatalog.AddModule(typeof(QuickLinkModule));
        }

        void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
