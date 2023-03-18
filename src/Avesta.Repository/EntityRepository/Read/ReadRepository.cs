using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Exceptions.Entity;
using Avesta.Share.Utilities;
using Avesta.Storage.Constant;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Read
{
    public class ReadRepository<TEntity, TId, TContext> : BaseReadRepository<TContext>, IReadRepository<TEntity, TId>
        where TId : class
        where TContext : AvestaDbContext, new()
        where TEntity : BaseEntity<TId>
    {
        public ReadRepository(TContext context) : base(context)
        {
        }




        public async Task<TEntity> First(string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await base.First<TEntity, TId>(navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await base.First<TEntity, TId>(includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await First(entities, track, exceptionRaiseIfNotExist);








        public async Task<TEntity> First(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await First<TEntity, TId>(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await First<TEntity, TId>(search, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> First(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await First<TEntity, TId>(entities, search, track, exceptionRaiseIfNotExist);







        public async Task<TEntity> Get(TId key
           , string navigationPropertyPath
           , bool track = true
           , bool exceptionRaiseIfNotExist = false)
               => await base.Get<TEntity, TId>(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(TId key
             , bool includeAllPath
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
                => await base.Get<TEntity, TId>(key, includeAllPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(IQueryable<TEntity> entities
             , TId key
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
                => await base.Get<TEntity, TId>(entities, key, track, exceptionRaiseIfNotExist);










        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await base.Get<TEntity, TId>(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await base.Get<TEntity, TId>(predicate, includeAllPath, track, exceptionRaiseIfNotExist);



        public async Task<TEntity> Get(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
                => await base.Get<TEntity, TId>(entities, predicate, track, exceptionRaiseIfNotExist);













        public async Task<IEnumerable<TEntity>> GetAll<TKey>(
            string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.GetAll<TEntity, TId, TKey>(navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> GetAll<TKey>(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.GetAll<TEntity, TId, TKey>(includeAllPath, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.GetAll<TEntity, TId, TKey>(entities, page, perPage, orderBy, orderbyDirection, track);







        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PerPage
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
                => await base.GetByIds<TEntity, TId, TKey>(ids, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.GetByIds<TEntity, TId, TKey>(ids, includeAllPath, page, perPage, orderBy, orderbyDirection, track);



        public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.GetByIds<TEntity, TId, TKey>(entities, ids, page, perPage, orderBy, orderbyDirection, track);






        public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
           , string navigationPropertyPath
           , int? page = null
           , int perPage = Pagination.PerPage
           , Func<TEntity, TKey> orderBy = null
           , OrderByDirection orderbyDirection = OrderByDirection.Ascending
           , bool track = false)
               => await base.Where<TEntity, TId, TKey>(search, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await base.Where<TEntity, TId, TKey>(search, includeAllPath, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
                => await Where<TEntity, TId, TKey>(entities, search, page, perPage, orderBy, orderbyDirection, track);


        public async Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
            => await Count<TEntity, TId>(where, navigationPropertyPath);

        public async Task<int> Count(Expression<Func<TEntity, bool>> where)
            => await Count<TEntity, TId>(where);


        public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
            => await Count<TEntity, TId>(entities, where);
        public async Task<int> Count()
            => await Count<TEntity, TId>();

    }

}
