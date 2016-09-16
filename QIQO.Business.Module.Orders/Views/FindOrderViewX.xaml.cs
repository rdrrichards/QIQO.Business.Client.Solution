using QIQO.Business.Module.Orders.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Orders.Views
{
    /// <summary>
    /// Interaction logic for FindOrderView.xaml
    /// </summary>
    public partial class FindOrderViewX : UserControl
    {
        public FindOrderViewX(FindOrderViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
