using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FCB.Extension.DependencyInjection
{
    public static class DependencyInjectionPlus
    {

        private static void RegisterScopedAssembly(Assembly assembly, IServiceCollection services)
        {
            List<Type> types = assembly
                .GetTypes()
                .Where(x => x.GetCustomAttributes<FCBDependencyAttribute>() != null && x.GetCustomAttributes<FCBDependencyAttribute>().Count().CompareTo(0) == 1)
                .ToList();



            foreach (Type item in types)
            {


                IEnumerable<FCBDependencyAttribute> attributes = item.GetCustomAttributes<FCBDependencyAttribute>();



                foreach (var attribute in attributes)
                {
                    if (attribute.InterfacesRepresentation == null || attribute.InterfacesRepresentation.Count() == 0)
                    {
                        foreach (var InterfaceRepresentation in item.GetInterfaces())
                            services.AddScoped(InterfaceRepresentation, item);
                    }
                    else
                    {
                        foreach (var InterfaceRepresentation in attribute.InterfacesRepresentation)
                            services.AddScoped(InterfaceRepresentation, item);
                    }

                }

            }

        }


        public static void AddInjectDependency(this IServiceCollection services, Assembly assembly)
        {

            if (services == null) throw new ArgumentNullException(nameof(services));


            if (assembly == null) throw new ArgumentNullException(nameof(assembly));


            RegisterScopedAssembly(assembly, services);

        }
    }
}