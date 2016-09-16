namespace QIQO.Business.Client.Core.UI
{
    public interface IReportService
    {
        void ExecuteReport(string reportName, string parameters = "");
    }
}
