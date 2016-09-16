using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for InformationBarView.xaml
    /// </summary>
    public partial class InformationBarView : UserControl
    {
        public InformationBarView(InformationBarViewModel view_model)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = view_model;
            };
        }
    }
}
