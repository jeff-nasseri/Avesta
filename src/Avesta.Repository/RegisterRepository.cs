using Avesta.Data.Context;
using Avesta.Data.Model;
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

        [Obsolete]
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
            var ripo = typeof(IEntityRepository<,>);
            foreach (var entity in entityTypes ?? Enumerable.Empty<Type>())
            {
                var ripoType = ripo.MakeGenericType(entity);
                var entityRipo = typeof(EntityRepository<,>);
                var entityRipoType = entityRipo.MakeGenericType(entity, typeof(TAvestaContext));
                services.AddScoped(ripoType, entityRipoType);
            }


            return services;

        }




        [Obsolete]
        public static IServiceCollection RegisterRepository<TAvestaContext>(this IServiceCollection services)
    where TAvestaContext : AvestaDbContext
        {


            List<Assembly> assemblies = new List<Assembly>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var path in Directory.GetFiles(assemblyFolder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFrom(path));
            }

            var listOfTypes = assemblies.Select(a => a.GetTypes());

            var entityTypes = (from types in listOfTypes from type in types select type)
                 .Where(TheType => TheType.IsClass
                 && !TheType.IsAbstract
                 && (TheType.IsSubclassOf(typeof(BaseEntity)))
                 && (TheType.BaseType == typeof(BaseEntity))
                 ).ToList();
            var ripo = typeof(IEntityRepository<,>);
            foreach (var entity in entityTypes ?? Enumerable.Empty<Type>())
            {
                var ripoType = ripo.MakeGenericType(entity);
                var entityRipo = typeof(EntityRepository<,>);
                var entityRipoType = entityRipo.MakeGenericType(entity, typeof(TAvestaContext));
                services.AddScoped(ripoType, entityRipoType);
            }


            return services;

        }







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


        public static IServiceCollection RegisterRepository<TId, TAvestaContext>(this IServiceCollection services, string dllFullname)
            where TId : class
            where TAvestaContext : AvestaDbContext
        {


            List<Assembly> assemblies = new List<Assembly>();
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var target = Assembly.LoadFrom(dllFullname);

            var entityTypes = target.GetTypes()
                 .Where(TheType => TheType.IsClass
                 && !TheType.IsAbstract
                 && (TheType.IsSubclassOf(typeof(BaseEntity)))
                 && (TheType.BaseType == typeof(BaseEntity))
                 ).ToList();
            var ripo = typeof(IEntityRepository<,>);
            foreach (var entity in entityTypes ?? Enumerable.Empty<Type>())
            {
                var ripoType = ripo.MakeGenericType(entity);
                var entityRipo = typeof(EntityRepository<,,>);
                var entityRipoType = entityRipo.MakeGenericType(entity, typeof(TAvestaContext), typeof(TId));
                services.AddScoped(ripoType, entityRipoType);
            }


            return services;

        }






    }
}
