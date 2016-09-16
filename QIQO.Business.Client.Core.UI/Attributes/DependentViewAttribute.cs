using System;

namespace QIQO.Business.Client.Core.UI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewAttribute : Attribute
    {
        public Type Type { get; set; }
        public string TargetRegionName { get; set; }

        public DependentViewAttribute(Type viewType, string targetRegionName)
        {
            Type = viewType;
            TargetRegionName = targetRegionName;
        }
    }
}
