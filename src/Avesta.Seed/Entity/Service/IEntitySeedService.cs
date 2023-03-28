using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Seed.Model;
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


    public interface IEntitySeedService<TId, TEntity, TAvestaDbContext>
      where TId : class
      where TEntity : BaseEntity<TId>
      where TAvestaDbContext : AvestaDbContext
    {
        Task<SeedResultModel> SeedEntity(int number = 100);
    }

}
