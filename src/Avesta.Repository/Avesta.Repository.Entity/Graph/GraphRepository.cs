
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository.Read;
using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Avesta.Data.Entity.Context;

namespace Avesta.Repository.EntityRepository.Graph
{
    public class GraphRepository<TEntity, TId, TContext> : BaseGraphRepository<TContext>, IGraphRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TContext : AvestaDbContext
    {


        public GraphRepository(TContext context) : base(context)
        {
        }


        public async Task<IEnumerable<dynamic>> GraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
                => await base.GraphQuery<TEntity, TId>(navigationPropertyPath, where, select, orderBy, page, perPage, track);


        public async Task<IEnumerable<dynamic>> GraphQuery(bool includeAllPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
                => await base.GraphQuery<TEntity, TId>(includeAllPath, select, orderBy, where, page, perPage, track);


        public async Task<IEnumerable<dynamic>> GraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
        , bool track = false)
                => await base.GraphQuery<TEntity, TId>(entities, where, select, orderBy, page, perPage, track);


    }
}
