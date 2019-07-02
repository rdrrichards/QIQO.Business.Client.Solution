using Prism.Events;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Wrappers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QIQO.Business.Module.Invoices.Services
{
    public class WorkingInvoiceService : IWorkingInvoiceService
    {
        private readonly IEventAggregator event_aggregator;

        private Dictionary<string, InvoiceWrapper> open_invoices;
        public WorkingInvoiceService(IEventAggregator event_aggtr)
        {
            event_aggregator = event_aggtr;
            Initialize();
        }

        private void Initialize()
        {
            open_invoices = new Dictionary<string, InvoiceWrapper>();
        }

        public InvoiceWrapper GetInvoice(string invoice_key)
        {
            if (open_invoices.ContainsKey(invoice_key))
            {
                return open_invoices[invoice_key];
            }
            else
            {
                return null;
            }
        }

        public bool OpenInvoice(InvoiceWrapper invoice)
        {
            if (!open_invoices.ContainsValue(invoice))
            {
                var new_key = GenInvoiceKey();
                invoice.InvoiceNumber = new_key;
                open_invoices.Add(new_key, invoice);
                event_aggregator.GetEvent<OpenInvoiceServiceEvent>().Publish(open_invoices.Count);
                return true;
            }
            return false;
        }

        public bool CloseInvoice(InvoiceWrapper invoice)
        {
            if (open_invoices.ContainsValue(invoice))
            {
                var key = open_invoices.FirstOrDefault(x => x.Value == invoice).Key;
                open_invoices.Remove(key);
                event_aggregator.GetEvent<OpenInvoiceServiceEvent>().Publish(open_invoices.Count);
                return true;
            }
            return false;
        }

        public ObservableCollection<InvoiceWrapper> GetWorkingInvoices()
        {
            var working_ords = new ObservableCollection<InvoiceWrapper>();
            foreach (var order_entry in open_invoices)
            {
                working_ords.Add(order_entry.Value);
            }
            return working_ords;
        }

        private string GenInvoiceKey()
        {
            return $"Open Invoice {open_invoices.Count + 1}";
        }
    }
}
