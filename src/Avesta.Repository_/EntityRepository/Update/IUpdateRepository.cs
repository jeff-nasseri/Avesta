using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Update
{
    public interface IUpdateRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false);
    }

}
