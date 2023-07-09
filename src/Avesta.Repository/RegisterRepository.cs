using Avesta.Data.Context;
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepository.Availability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Graph;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Update;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Share.Model;
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



        public static IServiceCollection RegisterRepositories<TId, TEntity, TAvestaContext>(this IServiceCollection services)
            where TId : class
            where TEntity : BaseEntity<TId>
            where TAvestaContext : AvestaDbContext
        {

            services.AddScoped<IReadRepository<TEntity, TId>, ReadRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<IUpdateRepository<TEntity, TId>, UpdateRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<ICreateRepository<TEntity, TId>, CreateRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<IDeleteRepository<TEntity, TId>, DeleteRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<IGraphRepository<TEntity, TId>, GraphRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<IAvailabilityRepository<TEntity, TId>, AvailabilityRepository<TEntity, TId, TAvestaContext>>();
            services.AddScoped<IEntityRepository<TEntity, TId>, EntityRepository<TEntity, TAvestaContext, TId>>();

            return services;
        }

        public static IServiceCollection RegisterRepositoriesByReflection(this IServiceCollection services, Type id, Type entity, Type context)
        {
            var methodInfo = typeof(RepositoryExtension).GetMethods().First(m => m.Name == nameof(RegisterRepositories));
            var method = methodInfo.MakeGenericMethod(id, entity, context);
            var result = method.Invoke(null, new object[] { services });

            return services;
        }


        public static IServiceCollection RegisterRepository<TId, TAvestaContext>(this IServiceCollection services, string dllFullname)
            where TId : class
            where TAvestaContext : AvestaDbContext
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
                services.RegisterRepositoriesByReflection(typeof(TId), entity, typeof(TAvestaContext));
            }


            return services;
        }






    }
}