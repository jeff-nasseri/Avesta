using Avesta.Data.Context;
using Avesta.Data.Entity.Context;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.Test.Src;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Repository.Test
{
    public class RepositoryResolver<TId, TEntity, TContext> : Program
        where TId : class
        where TEntity : BaseEntity<TId>
        where TContext : AvestaDbContext
    {

        public RepositoryResolver() => Start();



        public TContext Context { get => Builder.GetRequiredService<TContext>(); }

        public IEntityRepository<TEntity, TId> ResolveRepository()
        {
            var repository = Builder.GetRequiredService<IEntityRepository<TEntity, TId>>();
            return repository;
        }

    }

}
