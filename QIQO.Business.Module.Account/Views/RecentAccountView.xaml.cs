using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for RecentAccountView.xaml
    /// </summary>
    public partial class RecentAccountView : UserControl
    {
        public RecentAccountView(RecentAccountViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
