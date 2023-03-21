using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Availability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Qraph;
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
            , IGraphRepository<TEntity, string> graphRepository
            , IUpdateRepository<TEntity, string> updateRepository) 
            : base(readRepository, createRepository, deleteRepository, availabilityRepository, graphRepository, updateRepository)
        {
        }
    }
}
