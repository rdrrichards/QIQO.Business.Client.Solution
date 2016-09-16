using QIQO.Business.Module.General.ViewModels;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Views
{
    /// <summary>
    /// Interaction logic for ContactView.xaml
    /// </summary>
    public partial class ContactView : UserControl
    {
        public ContactView()
        {
            InitializeComponent();
            DataContext = new ContactViewModel();
        }
    }
}
