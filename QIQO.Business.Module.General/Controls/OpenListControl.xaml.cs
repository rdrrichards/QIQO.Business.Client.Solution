using QIQO.Business.Module.General.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Controls
{
    /// <summary>
    /// Interaction logic for OpenListControl.xaml
    /// </summary>
    public partial class OpenListControl : UserControl
    {
        public OpenListControl()
        {
            InitializeComponent();
        }

        public string OpenHeaderMessage
        {
            get { return (string)GetValue(OpenHeaderMessageProperty); }
            set { SetValue(OpenHeaderMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenHeaderMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenHeaderMessageProperty =
            DependencyProperty.Register("OpenHeaderMessage", typeof(string), typeof(OpenListControl), new PropertyMetadata("Open Items"));

        public ObservableCollection<BusinessItem> OpenItems
        {
            get { return (ObservableCollection<BusinessItem>)GetValue(BusinessItemsProperty); }
            set { SetValue(BusinessItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusinessItemsProperty =
            DependencyProperty.Register("OpenItems", typeof(ObservableCollection<BusinessItem>), typeof(OpenListControl), new PropertyMetadata(new ObservableCollection<BusinessItem>()));

        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRefreshingProperty =
            DependencyProperty.Register("IsRefreshing", typeof(bool), typeof(OpenListControl), new PropertyMetadata(false));


        public object SelectedOpenItem
        {
            get { return (object)GetValue(SelectedOpenItemProperty); }
            set { SetValue(SelectedOpenItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedOpenItemProperty =
            DependencyProperty.Register("SelectedOpenItem", typeof(object), typeof(OpenListControl), new PropertyMetadata(null));


        public int SelectedOpenItemIndex
        {
            get { return (int)GetValue(SelectedOpenItemIndexProperty); }
            set { SetValue(SelectedOpenItemIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedOpenItemIndexProperty =
            DependencyProperty.Register("SelectedOpenItemIndex", typeof(int), typeof(OpenListControl), new PropertyMetadata(0));
    }
}
