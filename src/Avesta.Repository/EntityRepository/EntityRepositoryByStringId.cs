using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Avability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Update;

namespace Avesta.Repository.EntityRepositoryRepository
{

    public class EntityRepository<TEntity, TContext> : EntityRepository<TEntity, TContext, string>
    where TEntity : BaseEntity<string>
    where TContext : AvestaDbContext
    {
        public EntityRepository(IReadRepository<TEntity, string> readRepository
            , ICreateRepository<TEntity, string> createRepository
            , IDeleteRepository<TEntity, string> deleteRepository
            , IAvailabilityRepository<TEntity, string> availabilityRepository
            , IUpdateRepository<TEntity, string> updateRepository
            , TContext context) : base(readRepository, createRepository, deleteRepository, availabilityRepository, updateRepository, context)
        {
        }
    }
}
