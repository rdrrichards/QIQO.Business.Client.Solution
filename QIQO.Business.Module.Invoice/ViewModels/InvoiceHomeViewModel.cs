using Prism.Commands;
using Prism.Regions;
using QIQO.Business.Client.Core.Infrastructure;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Invoices.Views;

namespace QIQO.Business.Module.Invoices.ViewModels
{
    public class InvoiceHomeViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;

        public InvoiceHomeViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            NewInvoiceCommand = new DelegateCommand(NewInvoice);
        }
        public DelegateCommand NewInvoiceCommand { get; set; }

        private void NewInvoice()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(InvoiceViewX).FullName);
        }
    }
}

