using QIQO.Business.Module.General.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Controls
{
    /// <summary>
    /// Interaction logic for InProcessListControl.xaml
    /// </summary>
    public partial class InProcessListControl : UserControl
    {
        public InProcessListControl()
        {
            InitializeComponent();
        }

        public string InProcessHeaderMessage
        {
            get { return (string)GetValue(InProcessHeaderMessageProperty); }
            set { SetValue(InProcessHeaderMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InProcessHeaderMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InProcessHeaderMessageProperty =
            DependencyProperty.Register("InProcessHeaderMessage", typeof(string), typeof(InProcessListControl), new PropertyMetadata("In Process Items"));

        public ObservableCollection<BusinessItem> InProcessItems
        {
            get { return (ObservableCollection<BusinessItem>)GetValue(BusinessItemsProperty); }
            set { SetValue(BusinessItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InProcessItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BusinessItemsProperty =
            DependencyProperty.Register("InProcessItems", typeof(ObservableCollection<BusinessItem>), typeof(InProcessListControl), new PropertyMetadata(new ObservableCollection<BusinessItem>()));

        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRefreshingProperty =
            DependencyProperty.Register("IsRefreshing", typeof(bool), typeof(InProcessListControl), new PropertyMetadata(false));


        public object SelectedInProcessItem
        {
            get { return (object)GetValue(SelectedInProcessItemProperty); }
            set { SetValue(SelectedInProcessItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedInProcessItemProperty =
            DependencyProperty.Register("SelectedInProcessItem", typeof(object), typeof(InProcessListControl), new PropertyMetadata(null));


        public int SelectedInProcessItemIndex
        {
            get { return (int)GetValue(SelectedInProcessItemIndexProperty); }
            set { SetValue(SelectedInProcessItemIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedInProcessItemIndexProperty =
            DependencyProperty.Register("SelectedInProcessItemIndex", typeof(int), typeof(InProcessListControl), new PropertyMetadata(0));
    }
}
