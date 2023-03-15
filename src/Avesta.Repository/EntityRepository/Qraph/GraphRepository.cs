using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Qraph
{
    public class GraphRepository<TEntity, TId, TContext> : BaseGraphRepository<TContext>, IGraphRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TContext : AvestaDbContext
    {


        public GraphRepository(TContext context) : base(context)
        {
        }


        public async Task<IEnumerable<TEntity>> QraphQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
        , bool track = false)
                => await base.QraphQuery<TEntity, TId>(navigationPropertyPath, where, select, orderBy, page, perPage, track);


        public async Task<IEnumerable<TEntity>> QraphQuery(string includeAllPath
            , bool where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
        , bool track = false)
                => await base.QraphQuery<TEntity, TId>(where, select, orderBy, includeAllPath, page, perPage, track);


        public async Task<IEnumerable<TEntity>> QraphQuery(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = 7
        , bool track = false)
                => await base.QraphQuery<TEntity, TId>(entities, where, select, orderBy, page, perPage, track);


    }
}
