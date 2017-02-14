using QIQO.Business.Module.Product.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Product.Views
{
    /// <summary>
    /// Interaction logic for ProductNavigationView.xaml
    /// </summary>
    public partial class ProductNavigationViewX : UserControl
    {
        public ProductNavigationViewX(ProductNavigationViewModelX view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
