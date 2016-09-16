using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderItemView.xaml
    /// </summary>
    public partial class OrderItemView : UserControl
    {
        public OrderItemView()
        {
            InitializeComponent();
            DataContext = new OrderItemViewModel();
        }
    }
}
