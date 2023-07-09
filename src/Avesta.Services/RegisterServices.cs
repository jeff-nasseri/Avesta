
using Avesta.Data.Entity.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avesta.Services.Graph;

namespace Avesta.Services
{

    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices<TId, TEntity>(this IServiceCollection services)
        where TId : class
        where TEntity : BaseEntity<TId>
        {

            services.AddScoped<IEntityGraphService<TId, TEntity>, EntityGraphService<TId, TEntity>>();

            return services;
        }

        public static IServiceCollection RegisterServicesByReflection(this IServiceCollection services, Type id, Type entity)
        {
            var methodInfo = typeof(ServiceExtension).GetMethods().First(m => m.Name == nameof(RegisterServices));
            var method = methodInfo.MakeGenericMethod(id, entity);
            var result = method.Invoke(null, new object[] { services });

            return services;
        }


        public static IServiceCollection RegisterService<TId>(this IServiceCollection services, string dllFullname)
            where TId : class
        {


            List<Assembly> assemblies = new List<Assembly>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var target = Assembly.LoadFrom(Path.Combine(assemblyFolder, dllFullname));

            var entityTypes = target.GetTypes()
                 .Where(TheType => TheType.IsClass
                 && !TheType.IsAbstract
                 && (TheType.IsSubclassOf(typeof(BaseEntity)))
                 && (TheType.BaseType == typeof(BaseEntity))
                 ).ToList();
            foreach (var entity in entityTypes ?? Enumerable.Empty<Type>())
            {
                services.RegisterServicesByReflection(typeof(TId), entity);
            }


            return services;
        }
    }

}
