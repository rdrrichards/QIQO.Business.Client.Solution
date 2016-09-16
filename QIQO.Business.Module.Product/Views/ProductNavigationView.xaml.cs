using QIQO.Business.Module.Product.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Product.Views
{
    /// <summary>
    /// Interaction logic for ProductNavigationView.xaml
    /// </summary>
    public partial class ProductNavigationView : UserControl
    {
        public ProductNavigationView()
        {
            InitializeComponent();
            DataContext = new ProductNavigationViewModel();
        }
    }
}
