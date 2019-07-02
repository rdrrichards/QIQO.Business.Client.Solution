using Prism.Regions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace QIQO.Business.Client.Core.UI
{
    public class TabItemOpenBehavior : Behavior<TabControl>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(TabItem.LoadedEvent, new RoutedEventHandler(TabItem_Loaded));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(TabItem.LoadedEvent, new RoutedEventHandler(TabItem_Loaded));
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            var region = RegionManager.GetObservableRegion(AssociatedObject).Value;
            if (region == null)
            {
                return;
            }

            var context = new NavigationContext(region.NavigationService, null);
            InvokeOnNavigatedTo(GetItemFromTabItem(e.OriginalSource), context);
        }

        object GetItemFromTabItem(object source)
        {
            var tabItem = source as TabItem;
            if (tabItem == null)
            {
                return null;
            }

            return tabItem.Content;
        }

        void InvokeOnNavigatedTo(object item, NavigationContext navigationContext)
        {
            var navigationAwareItem = item as INavigationAware;
            if (navigationAwareItem != null)
            {
                navigationAwareItem.OnNavigatedTo(navigationContext);
            }

            var frameworkElement = item as FrameworkElement;
            if (frameworkElement != null)
            {
                var navigationAwareDataContext = frameworkElement.DataContext as INavigationAware;
                if (navigationAwareDataContext != null)
                {
                    navigationAwareDataContext.OnNavigatedTo(navigationContext);
                }
            }
        }
    }
}
