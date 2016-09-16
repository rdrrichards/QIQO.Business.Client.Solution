using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    public partial class OrderHomeView : UserControl
    {
        public OrderHomeView(OrderHomeViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
