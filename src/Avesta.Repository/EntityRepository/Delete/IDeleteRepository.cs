using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Extensions.Core.SchemaObjectModel;

namespace Avesta.Repository.EntityRepository.Delete
{
    public interface IDeleteRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false);
        Task Delete(TId id, bool exceptionRaiseIfNotExist = false);
        Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false);

        Task DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteRange(Expression<Func<TEntity, bool>> where);

        Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false);
    }

}
