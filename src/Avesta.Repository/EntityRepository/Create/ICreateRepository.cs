using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Create
{
    public interface ICreateRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        Task Insert(TEntity entity);
        Task InsertRange(IEnumerable<TEntity> entities);
        Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities);
        Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities);
        Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities);
    }

}
