using QIQO.Business.Module.Account.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Account.Views
{
    /// <summary>
    /// Interaction logic for WorkingAccountView.xaml
    /// </summary>
    public partial class WorkingAccountView : UserControl
    {
        public WorkingAccountView(WorkingAccountViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
