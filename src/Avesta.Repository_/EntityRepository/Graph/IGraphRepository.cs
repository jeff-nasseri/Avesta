using Avesta.Data.Entity.Model;
using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Graph
{
    public interface IGraphRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false);


        Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false);


        Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false);


    }
}
