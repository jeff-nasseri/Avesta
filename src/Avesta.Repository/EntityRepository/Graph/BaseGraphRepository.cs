using Avesta.Data.Context;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Share.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Avesta.Constant;
using Avesta.Data.Entity.Context;

namespace Avesta.Repository.EntityRepository.Graph
{
    public class BaseGraphRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {



        public BaseGraphRepository(TContext context) : base(context)
        {
        }




        public async Task<IEnumerable<dynamic>> GraphQuery<TEntity, TId>(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await GraphQuery<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), where, select, orderBy, page, perPage, track);


        public async Task<IEnumerable<dynamic>> GraphQuery<TEntity, TId>(bool includeAllPath
            , string select
            , string orderBy
            , string where
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await GraphQuery<TEntity, TId>(base.IncludeAll<TEntity, TId>(), where, select, orderBy, page, perPage, track) :
                        await GraphQuery<TEntity, TId>(base.Query<TEntity, TId>(), where, select, orderBy, page, perPage, track);


        public async Task<IEnumerable<dynamic>> GraphQuery<TEntity, TId>(IQueryable<TEntity> entities
            , string where
            , string select
            , string orderBy
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {

            if (!track)
                entities = entities.AsNoTracking();

            var data = entities.Where(where);

            if (page != null)
            {
                PaginationUtils.Paginate(out int skip, perPage, page);
                data = data.Skip(skip).Take(perPage);
            }

            var result = await data.OrderBy(orderBy).Select(select).ToDynamicListAsync();

            return result;
        }



    }
}
