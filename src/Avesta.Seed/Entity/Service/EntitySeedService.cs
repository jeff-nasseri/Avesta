using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Repository.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed.Entity.Service
{

    public interface IEntitySeedService
    {
        Task Seed<TEntity>(TEntity entity) where TEntity : BaseEntity<string>;
        Task SeedRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<string>;
    }


    public class EntitySeedService<TContext> : IEntitySeedService
        where TContext : DbContext
    {
        readonly EntityRepository<TContext> _entityRepository;
        public EntitySeedService(EntityRepository<TContext> entityRepository)
        {
            _entityRepository = entityRepository;
        }



        public async Task Seed<TEntity>(TEntity entity) where TEntity : BaseEntity<string>
        {
            await _entityRepository.InsertAsync(entity);
        }



        public async Task SeedRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<string>
        {
            await _entityRepository.InsertRange(entities);
        }



    }
}
