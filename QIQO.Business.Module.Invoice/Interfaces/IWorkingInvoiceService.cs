using QIQO.Business.Client.Wrappers;
using System.Collections.ObjectModel;

namespace QIQO.Business.Module.Invoices.Services
{
    public interface IWorkingInvoiceService
    {
        InvoiceWrapper GetInvoice(string invoice_key);
        bool OpenInvoice(InvoiceWrapper invoice);
        ObservableCollection<InvoiceWrapper> GetWorkingInvoices();
        bool CloseInvoice(InvoiceWrapper invoice);
    }
}
