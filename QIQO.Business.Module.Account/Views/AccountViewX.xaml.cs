using Microsoft.Practices.Unity;
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
        //public AccountViewX() : this(new AccountViewModel(Unity.Container.Resolve<IEventAggregator>(),
        //    Unity.Container.Resolve<IServiceFactory>(),
        //    Unity.Container.Resolve<IRegionManager>(),
        //    Unity.Container.Resolve<IProductListService>(),
        //    Unity.Container.Resolve<IStateListService>(),
        //    Unity.Container.Resolve<IReportService>(),
        //    Unity.Container.Resolve<IAccountEntityService>()))
        //{ }
        public AccountViewX(AccountViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
