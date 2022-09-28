using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepositoryRepository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository
{
    public static class RepositoryExtension
    {
        public static IServiceCollection RegisterRepository<TAvestaUser, TAvestaContext>(this IServiceCollection services)
    where TAvestaUser : AvestaUser
    where TAvestaContext : AvestaDbContext
        {


            List<Assembly> assemblies = new List<Assembly>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }

            var entityTypes = Assembly.GetAssembly(typeof(BaseEntity<>))?.GetTypes()
                 .Where(TheType => TheType.IsClass
                 && !TheType.IsAbstract
                 && (TheType.IsSubclassOf(typeof(BaseEntity)) || TheType.IsSubclassOf(typeof(TAvestaUser)))
                 && (TheType.BaseType == typeof(BaseEntity) || TheType.BaseType == typeof(TAvestaUser))
                 );
            var ripo = typeof(IRepository<>);
            foreach (var entity in entityTypes ?? Enumerable.Empty<Type>())
            {
                var ripoType = ripo.MakeGenericType(entity);
                var entityRipo = typeof(EntityRepository<,>);
                var entityRipoType = entityRipo.MakeGenericType(entity, typeof(TAvestaContext));
                services.AddScoped(ripoType, entityRipoType);
            }


            return services;

        }
    }
}
