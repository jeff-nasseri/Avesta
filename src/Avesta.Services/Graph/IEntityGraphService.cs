using Avesta.Data.Model;
using Avesta.Share.Model;
using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Graph
{
    public interface IEntityGraphService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {


        Task<IEnumerable<TModel>> GraphQuery(string navigationPropertyPath
           , string where
           , string select
           , string orderBy
           , int? page = null
           , int perPage = Pagination.PerPage
       , bool track = false);


        Task<IEnumerable<TModel>> GraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false);

        Task<IEnumerable<TModel>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false);


    }

}
