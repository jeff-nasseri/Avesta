using Avesta.Data.Model;
using Avesta.Storage.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Qraph
{
    public interface IGraphRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        Task<IEnumerable<TEntity>> QraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
            , bool track = false);


        Task<IEnumerable<TEntity>> QraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
            , bool track = false);


        Task<IEnumerable<TEntity>> QraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
            , bool track = false);


    }
}
