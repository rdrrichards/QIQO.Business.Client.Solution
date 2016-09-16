using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for AttributeView.xaml
    /// </summary>
    public partial class AttributeView : UserControl
    {
        public AttributeView()
        {
            InitializeComponent();
            DataContext = new AttributeViewModel();
        }
    }
}
