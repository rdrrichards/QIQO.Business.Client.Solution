using QIQO.Business.Client.Core.UI;
using System.Windows.Controls.Ribbon;

namespace QIQO.Business.Client.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : RibbonWindow
    {
        public Shell(IShellViewModel view_model)
        {
            InitializeComponent();
            DataContext = view_model;
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RibbonApplicationMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
