using Prism.Commands;

namespace QIQO.Business.Client.Core
{
    public static class ApplicationCommands
    {
        public static CompositeCommand NavigateCommand = new CompositeCommand();
        public static CompositeCommand OrdersNavigateCommand = new CompositeCommand();
        public static CompositeCommand InvoicesNavigateCommand = new CompositeCommand();
        public static CompositeCommand NewOrderNavigateCommand = new CompositeCommand();

        public static CompositeCommand SaveOrderCommand = new CompositeCommand(true);
        public static CompositeCommand DeleteOrderCommand = new CompositeCommand(true);
        public static CompositeCommand CancelOrderCommand = new CompositeCommand(true);
        public static CompositeCommand PrintOrderCommand = new CompositeCommand(true);

        public static CompositeCommand SaveAccountCommand = new CompositeCommand();
        public static CompositeCommand FindAccountCommand = new CompositeCommand();
        public static CompositeCommand DeleteAccountCommand = new CompositeCommand();
        public static CompositeCommand CancelAccountCommand = new CompositeCommand();
        public static CompositeCommand PrintAccountCommand = new CompositeCommand();

        public static CompositeCommand SaveProductCommand = new CompositeCommand();
        public static CompositeCommand DeleteProductCommand = new CompositeCommand();

        public static CompositeCommand CompanySaveCommand = new CompositeCommand();
        public static CompositeCommand CompanyCancelCommand = new CompositeCommand();

        public static CompositeCommand CompanyEditEmployeeCommand = new CompositeCommand();
        public static CompositeCommand CompanyAddEmployeeCommand = new CompositeCommand();
        public static CompositeCommand CompanyDeleteEmployeeCommand = new CompositeCommand();
        public static CompositeCommand CompanyFindEmployeeCommand = new CompositeCommand();

        public static CompositeCommand CompanyEditCOACommand = new CompositeCommand();
        public static CompositeCommand CompanyAddCOACommand = new CompositeCommand();
        public static CompositeCommand CompanyDeleteCOACommand = new CompositeCommand();

        public static CompositeCommand CompanyEditAttributeCommand = new CompositeCommand();
        public static CompositeCommand CompanyAddAttributeCommand = new CompositeCommand();
        public static CompositeCommand CompanyDeleteAttributeCommand = new CompositeCommand();

        public static CompositeCommand DashboardRefreshCommand = new CompositeCommand();

        public static CompositeCommand SaveInvoiceCommand = new CompositeCommand(true);
        public static CompositeCommand DeleteInvoiceCommand = new CompositeCommand(true);
        public static CompositeCommand CancelInvoiceCommand = new CompositeCommand(true);
        public static CompositeCommand PrintInvoiceCommand = new CompositeCommand(true);
    }
}
