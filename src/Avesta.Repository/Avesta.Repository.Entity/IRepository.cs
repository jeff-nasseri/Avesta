using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository.Availability;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Repository.EntityRepository.Graph;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Repository.EntityRepository.Update;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository
{



    public interface IEntityRepository<TEntity, TId> : IReadRepository<TEntity, TId>
        , ICreateRepository<TEntity, TId>
        , IDeleteRepository<TEntity, TId>
        , IUpdateRepository<TEntity, TId>
        , IAvailabilityRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
    }


}
