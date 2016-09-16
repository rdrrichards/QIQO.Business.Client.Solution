
using System.Windows;

namespace QIQO.Business.Client.UIX.Views
{
    public partial class CompanyChooserView : Window
    {
        public CompanyChooserView(CompanyChooserViewModel view_model)
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                DataContext = view_model;
            };
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CompanyList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenMain();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            OpenMain();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenMain()
        {
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
