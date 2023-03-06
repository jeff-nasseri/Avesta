using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.Test.Src;
using Microsoft.Extensions.DependencyInjection;

namespace Avesta.Repository.Test
{
    public class RepositoryResolver<TEntity, TContext> : Program
        where TEntity : BaseEntity
        where TContext : AvestaDbContext
    {

        public RepositoryResolver() => Start();



        public TContext Context { get => Builder.GetRequiredService<TContext>(); }

        public IRepository<TEntity> ResolveRepository()
        {
            var repository = Builder.GetRequiredService<IRepository<TEntity>>();
            return repository;
        }

    }

}
