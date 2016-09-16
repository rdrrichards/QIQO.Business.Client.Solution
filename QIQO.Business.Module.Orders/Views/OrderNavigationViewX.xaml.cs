using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderNavigationViewX.xaml
    /// </summary>
    public partial class OrderNavigationViewX : UserControl
    {
        public OrderNavigationViewX(OrderNavigationViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
