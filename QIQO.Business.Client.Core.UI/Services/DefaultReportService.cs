using System.Collections.Specialized;
using System.Configuration;

namespace QIQO.Business.Client.Core.UI
{
    public class DefaultReportService : IReportService
    {
        protected readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;
        private string serverName = @"http://localhost/reportservice";
        private string reportCommand = "Render";
        public DefaultReportService()
        {
            Initialize();
        }

        private void Initialize()
        {
            serverName = appSettings["report_server_name"].ToString() + "?";
            reportCommand = $"&rs:Command={appSettings["report_server_default_command"].ToString()}";
        }

        public void ExecuteReport(string reportName, string parameters = "")
        {
            string reportPath = appSettings[reportName].ToString();

            if (parameters.Length > 0) parameters = $"&{parameters}";

            string fullReportUrl = serverName + reportPath + reportCommand + parameters;

            try
            {
                System.Diagnostics.Process.Start(fullReportUrl);
            }
            catch
            {

            }
        }
    }

    public static class Reports
    {
        public const string Order = "order_report";
        public const string OpenAccountOrders = "open_order_by_account_report";
        public const string OpenCompanyOrders = "open_order_by_company_report";

        public const string Invoice = "invoice_report";
        public const string Account = "account_report";
        public const string Product = "product_report";
    }
}
