using QIQO.Business.Module.General.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace QIQO.Business.Module.General.Controls
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public SearchControl()
        {
            InitializeComponent();
        }
        public string SearchTerm
        {
            get { return (string)GetValue(SearchTermProperty); }
            set { SetValue(SearchTermProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchTerm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTermProperty =
            DependencyProperty.Register("SearchTerm", typeof(string), typeof(SearchControl), new PropertyMetadata(""));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonContentProperty =
            DependencyProperty.Register("ButtonContent", typeof(string), typeof(SearchControl), new PropertyMetadata("Find"));

        public ObservableCollection<BusinessItem> FoundItems
        {
            get { return (ObservableCollection<BusinessItem>)GetValue(FoundItemsProperty); }
            set { SetValue(FoundItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FoundItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FoundItemsProperty =
            DependencyProperty.Register("FoundItems", typeof(ObservableCollection<BusinessItem>), typeof(SearchControl), new PropertyMetadata(new ObservableCollection<BusinessItem>()));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBusyProperty =
            DependencyProperty.Register("IsBusy", typeof(bool), typeof(SearchControl), new PropertyMetadata(false));


        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(SearchControl), new PropertyMetadata(null));


        public int SelectedItemIndex
        {
            get { return (int)GetValue(SelectedItemIndexProperty); }
            set { SetValue(SelectedItemIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemIndexProperty =
            DependencyProperty.Register("SelectedItemIndex", typeof(int), typeof(SearchControl), new PropertyMetadata(0));
    }
}
