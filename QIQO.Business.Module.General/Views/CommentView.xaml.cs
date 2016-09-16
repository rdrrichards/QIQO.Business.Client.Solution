using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for CommentView.xaml
    /// </summary>
    public partial class CommentView : UserControl
    {
        public CommentView()
        {
            InitializeComponent();
            DataContext = new CommentViewModel();
        }
    }
}
