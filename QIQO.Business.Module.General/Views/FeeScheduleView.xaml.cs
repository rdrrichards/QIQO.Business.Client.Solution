using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for FeeScheduleView.xaml
    /// </summary>
    public partial class FeeScheduleView : UserControl
    {
        public FeeScheduleView()
        {
            InitializeComponent();
            DataContext = new FeeScheduleViewModel();
        }
    }
}
