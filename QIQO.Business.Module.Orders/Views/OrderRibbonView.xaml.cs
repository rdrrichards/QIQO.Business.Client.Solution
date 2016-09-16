using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderRibbonView.xaml
    /// </summary>
    public partial class OrderRibbonView : RibbonTab
    {
        public OrderRibbonView()
        {
            InitializeComponent();
            DataContext = new OrderRibbonViewModel();
        }
    }
}
