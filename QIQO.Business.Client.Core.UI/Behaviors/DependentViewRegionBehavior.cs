using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace QIQO.Business.Client.Core.UI
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        readonly Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        public const string BehaviorKey = "DependentViewRegionBehavior";

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var tabList = new List<DependentViewInfo>();

                foreach (var newView in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();

                    if (_dependentViewCache.ContainsKey(newView))
                    {
                        dependentViews = _dependentViewCache[newView];
                    }
                    else
                    {
                        foreach (var atr in GetCustomAttributes<DependentViewAttribute>(newView.GetType()))
                        {
                            var info = CreateDependentViewInfo(atr);

                            if (info.View is ISupportDataContext && newView is ISupportDataContext)
                            {
                                ((ISupportDataContext)info.View).DataContext = ((ISupportDataContext)newView).DataContext;
                            }

                            dependentViews.Add(info);
                        }

                        if (!_dependentViewCache.ContainsKey(newView))
                        {
                            _dependentViewCache.Add(newView, dependentViews);
                        }
                    }

                    dependentViews.ForEach(item => Region.RegionManager.Regions[item.TargetRegionName].Add(item.View));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldView in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldView))
                    {
                        _dependentViewCache[oldView].ForEach(x => Region.RegionManager.Regions[x.TargetRegionName].Remove(x.View));

                        if (!ShouldKeepAlive(oldView))
                        {
                            _dependentViewCache.Remove(oldView);
                        }
                    }
                }
            }
        }

        private DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {
            var info = new DependentViewInfo();

            info.TargetRegionName = attribute.TargetRegionName;

            if (attribute.Type != null)
            {
                info.View = Activator.CreateInstance(attribute.Type);
            }

            return info;
        }

        private static bool ShouldKeepAlive(object view)
        {
            var lifetime = GetItemOrContextLifetime(view);
            if (lifetime != null)
            {
                return lifetime.KeepAlive;
            }

            var lifetimeAttribute = GetItemOrContextLifetimeAttribute(view);
            if (lifetimeAttribute != null)
            {
                return lifetimeAttribute.KeepAlive;
            }

            return true;
        }

        private static RegionMemberLifetimeAttribute GetItemOrContextLifetimeAttribute(object view)
        {
            var lifetimeAttribute = GetCustomAttributes<RegionMemberLifetimeAttribute>(view.GetType()).FirstOrDefault();
            if (lifetimeAttribute != null)
            {
                return lifetimeAttribute;
            }

            var frameworkElement = view as FrameworkElement;
            if (frameworkElement != null && frameworkElement.DataContext != null)
            {
                var dataContext = frameworkElement.DataContext;
                var contextLifetimeAttribute =
                    GetCustomAttributes<RegionMemberLifetimeAttribute>(dataContext.GetType()).FirstOrDefault();
                return contextLifetimeAttribute;
            }

            return null;
        }

        private static IRegionMemberLifetime GetItemOrContextLifetime(object view)
        {
            var regionLifetime = view as IRegionMemberLifetime;
            if (regionLifetime != null)
            {
                return regionLifetime;
            }

            var frameworkElement = view as FrameworkElement;
            if (frameworkElement != null)
            {
                return frameworkElement.DataContext as IRegionMemberLifetime;
            }

            return null;
        }

        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    internal class DependentViewInfo
    {
        public object View { get; set; }
        public string TargetRegionName { get; set; }
    }
}
