using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for TitleBarView.xaml
    /// </summary>
    public partial class TitleBarView : UserControl
    {
        public TitleBarView(TitleBarViewModel view_model)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = view_model;
            };
        }
    }
}
