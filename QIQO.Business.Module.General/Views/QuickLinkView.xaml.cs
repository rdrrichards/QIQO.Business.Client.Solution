using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    public partial class QuickLinkView : UserControl
    {
        public QuickLinkView(QuickLinkViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
