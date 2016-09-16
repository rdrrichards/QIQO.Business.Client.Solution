using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for OrderFinderView.xaml
    /// </summary>
    public partial class OrderFinderView : UserControl
    {
        public OrderFinderView()
        {
            InitializeComponent();
            DataContext = new OrderFinderViewModel();
        }
    }
}
