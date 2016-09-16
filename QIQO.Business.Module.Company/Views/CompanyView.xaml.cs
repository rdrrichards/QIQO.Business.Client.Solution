using QIQO.Business.Module.Company.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.Company.Views
{
    /// <summary>
    /// Interaction logic for CompanyView.xaml
    /// </summary>
    public partial class CompanyView : UserControl
    {
        public CompanyView(CompanyViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
