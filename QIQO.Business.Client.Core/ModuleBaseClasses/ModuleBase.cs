using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace QIQO.Business.Client.Core
{
    public abstract class ModuleBase : IModule
    {
        protected IRegionManager RegionManager { get; set; }
        protected IUnityContainer UnityContainer { get; set; }

        public ModuleBase(IUnityContainer container, IRegionManager region_manager)
        {
            UnityContainer = container;
            RegionManager = region_manager;
        }

        public abstract void Initialize();

        //protected abstract void RegisterTypes();
    }
}
