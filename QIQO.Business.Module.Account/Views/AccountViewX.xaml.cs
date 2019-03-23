using CommonServiceLocator;
using Prism.Events;
using Prism.Regions;
using QIQO.Business.Client.Core;
using QIQO.Business.Client.Core.UI;
using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountViewX : UserControl
    {
        //public AccountViewX() : this(new AccountViewModel(ServiceLocator.Current.GetInstance<IEventAggregator>(),
        //    ServiceLocator.Current.GetInstance<IServiceFactory>(),
        //    ServiceLocator.Current.GetInstance<IRegionManager>(),
        //    ServiceLocator.Current.GetInstance<IProductListService>(),
        //    ServiceLocator.Current.GetInstance<IStateListService>(),
        //    ServiceLocator.Current.GetInstance<IReportService>(),
        //    ServiceLocator.Current.GetInstance<IAccountEntityService>()))
        //{ }
        public AccountViewX(AccountViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
