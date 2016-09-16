using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for CalendarBarView.xaml
    /// </summary>
    public partial class CalendarBarView : UserControl
    {
        public CalendarBarView(CalendarBarViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }
    }
}
