using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace QIQO.Business.Client.Core
{
    public class QIQOModuleInitializer : ModuleInitializer
    {
        // Use this when you want to apply security around the creation of modules based on AD roles
        public QIQOModuleInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade) : 
            base (serviceLocator, loggerFacade)
        {

        }

        protected override IModule CreateModule(ModuleInfo moduleInfo)
        {
            if (ModuleIsInUserRole(moduleInfo))
                return base.CreateModule(moduleInfo);

            return null;
        }

        private bool ModuleIsInUserRole(ModuleInfo moduleInfo)
        {
            bool isInRole = true; // This should really be false by default

            string role = GetModuleRole(moduleInfo);
            
            if (role != null && !ClaimsPrincipal.Current.IsInRole(role))
            {
                isInRole = false;
            }

            return isInRole;
        }

        private string GetModuleRole(ModuleInfo moduleInfo)
        {
            var type = Type.GetType(moduleInfo.ModuleType);

            foreach (var attr in GetCustomAttribute<RolesAttribute>(type))
            {
                return attr.Role;
            }

            return null;
        }

        private IEnumerable<T> GetCustomAttribute<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RolesAttribute : Attribute
    {
        public string Role { get; set; }

        public RolesAttribute(string role)
        {
            Role = role;
        }
    }
}
