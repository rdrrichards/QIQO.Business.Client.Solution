using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for OrderNavigationView.xaml
    /// </summary>
    public partial class OrderNavigationView : UserControl
    {
        public OrderNavigationView()
        {
            InitializeComponent();
            DataContext = new OrderNavigationViewModel();
        }
    }
}
