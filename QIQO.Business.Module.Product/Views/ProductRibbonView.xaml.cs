using QIQO.Business.Module.Product.ViewModels;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Module.Product.Views
{
    /// <summary>
    /// Interaction logic for ProductRibbonView.xaml
    /// </summary>
    public partial class ProductRibbonView : RibbonTab
    {
        public ProductRibbonView()
        {
            InitializeComponent();
            DataContext = new ProductRibbonViewModel();
        }
    }
}
