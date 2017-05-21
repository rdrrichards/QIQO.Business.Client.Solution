using QIQO.Business.Module.General.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Controls
{
    /// <summary>
    /// Interaction logic for RecentListControl.xaml
    /// </summary>
    public partial class RecentListControl : UserControl
    {
        public RecentListControl()
        {
            InitializeComponent();
        }

        public string HeaderMessage
        {
            get { return (string)GetValue(HeaderMessageProperty); }
            set { SetValue(HeaderMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderMessageProperty =
            DependencyProperty.Register("HeaderMessage", typeof(string), typeof(RecentListControl), new PropertyMetadata("Recent Items"));

        public ObservableCollection<BusinessItem> RecentItems
        {
            get { return (ObservableCollection<BusinessItem>)GetValue(RecentItemsProperty); }
            set { SetValue(RecentItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FoundItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RecentItemsProperty =
            DependencyProperty.Register("RecentItems", typeof(ObservableCollection<BusinessItem>), typeof(RecentListControl), new PropertyMetadata(new ObservableCollection<BusinessItem>()));

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(RecentListControl), new PropertyMetadata(false));


        public object SelectedRecentItem
        {
            get { return (object)GetValue(SelectedRecentItemProperty); }
            set { SetValue(SelectedRecentItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedRecentItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRecentItemProperty =
            DependencyProperty.Register("SelectedRecentItem", typeof(object), typeof(RecentListControl), new PropertyMetadata(null));


        public int SelectedRecentItemIndex
        {
            get { return (int)GetValue(SelectedRecentItemIndexProperty); }
            set { SetValue(SelectedRecentItemIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedRecentItemIndexProperty =
            DependencyProperty.Register("SelectedRecentItemIndex", typeof(int), typeof(RecentListControl), new PropertyMetadata(0));
    }
}
