using AutoMapper.Execution;
using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Exceptions.Entity;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Share.Extensions;
using Avesta.Share.Utilities;
using Avesta.Storage.Constant;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Read
{
    public class BaseReadRepository<TContext> : BaseRepository<TContext>
        where TContext : AvestaDbContext
    {

        public BaseReadRepository(TContext context) : base(context)
        {
        }


        public async Task<TEntity> First<TEntity, TId>(string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await First<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), track, exceptionRaiseIfNotExist);


        public async Task<TEntity> First<TEntity, TId>(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await First<TEntity, TId>(base.IncludeAll<TEntity, TId>(), track, exceptionRaiseIfNotExist) :
                    await First<TEntity, TId>(base.Query<TEntity, TId>(), track, exceptionRaiseIfNotExist);


        public async Task<TEntity> First<TEntity, TId>(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var entity = await entities.FirstOrDefaultAsync();
            if (exceptionRaiseIfNotExist && entity == null)
                throw new CanNotFoundEntityException("first entity not found.");

            if (!track)
                base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

            return entity;
        }












        public async Task<TEntity> First<TEntity, TId>(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await First<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), search, track, exceptionRaiseIfNotExist);


        public async Task<TEntity> First<TEntity, TId>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await First<TEntity, TId>(base.IncludeAll<TEntity, TId>(), search, track, exceptionRaiseIfNotExist) :
                    await First<TEntity, TId>(base.Query<TEntity, TId>(), search, track, exceptionRaiseIfNotExist);


        public async Task<TEntity> First<TEntity, TId>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var entity = await entities.FirstOrDefaultAsync(search);

            if (exceptionRaiseIfNotExist && entity == null)
                throw new CanNotFoundEntityException(search);


            if (!track)
                base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

            return entity;
        }
















        public async Task<TEntity> Get<TEntity, TId>(TId id
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Get<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), id, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get<TEntity, TId>(TId id
             , bool includeAllPath
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
             where TId : class
             where TEntity : BaseEntity<TId>
                => includeAllPath ? await Get<TEntity, TId>(base.IncludeAll<TEntity, TId>(), id, track, exceptionRaiseIfNotExist) :
                    await Get<TEntity, TId>(base.Query<TEntity, TId>(), id, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get<TEntity, TId>(IQueryable<TEntity> entities
             , TId id
             , bool track = true
             , bool exceptionRaiseIfNotExist = false)
             where TId : class
             where TEntity : BaseEntity<TId>
        {
            var entity = await entities.SingleOrDefaultAsync(e => e.ID == id);

            if (exceptionRaiseIfNotExist && entities == null)
                throw new CanNotFoundEntityException(id);

            if (!track)
                base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

            return entity;
        }










        public async Task<TEntity> Get<TEntity, TId>(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Get<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), predicate, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get<TEntity, TId>(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await Get<TEntity, TId>(base.IncludeAll<TEntity, TId>(), predicate, track, exceptionRaiseIfNotExist) :
                     await Get<TEntity, TId>(base.Query<TEntity, TId>(), predicate, track, exceptionRaiseIfNotExist);

        public async Task<TEntity> Get<TEntity, TId>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var entity = await entities.SingleOrDefaultAsync(predicate);

            if (exceptionRaiseIfNotExist && entity == null)
                throw new CanNotFoundEntityException(predicate.ToString());

            if (!track)
                base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

            return entity;
        }













        public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await GetAll<TEntity, TId, TKey>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await GetAll<TEntity, TId, TKey>(base.IncludeAll<TEntity, TId>(), page, perPage, orderBy, orderbyDirection, track) :
                     await GetAll<TEntity, TId, TKey>(base.Query<TEntity, TId>(), page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {

            var data = default(IEnumerable<TEntity>);

            if (!track)
                entities = entities.AsNoTracking();

            if (page == null)
                data = entities.ToList();
            else
            {
                PaginationUtils.Paginate(out int skip, perPage, page);
                data = await entities.Skip(skip).Take(perPage).ToListAsync();
            }


            if (orderBy != null)
                data = data.OrderBy<TEntity, TKey>(orderBy, orderbyDirection).ToList();

            return data;
        }














        public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IEnumerable<TId> ids
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await GetByIds<TEntity, TId, TKey>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), ids, page, perPage, orderBy, orderbyDirection, track);

        public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await GetByIds<TEntity, TId, TKey>(base.IncludeAll<TEntity, TId>(), ids, page, perPage, orderBy, orderbyDirection, track) :
                      await GetByIds<TEntity, TId, TKey>(base.Query<TEntity, TId>(), ids, page, perPage, orderBy, orderbyDirection, track);



        public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            _ = ids ?? throw new ArgumentNullException("ids can not be null.");

            if (!track)
                entities = entities.AsNoTracking();

            var data = await ids.ForEach<TId, TEntity>(async (id) =>
            {
                var entity = await Get<TEntity, TId>(entities, id, track, exceptionRaiseIfNotExist: false);
                return entity;
            });

            if (page == null)
                data = entities.ToList();
            else
            {
                PaginationUtils.Paginate(out int skip, perPage, page);
                data = data.Skip(skip).Take(perPage).ToList();
            }

            if (orderBy != null)
                data = data.OrderBy(orderBy, orderbyDirection).ToList();

            return data;
        }








        public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Where<TEntity, TId, TKey>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), search, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
                => includeAllPath ? await Where<TEntity, TId, TKey>(base.IncludeAll<TEntity, TId>(), search, page, perPage, orderBy, orderbyDirection, track) :
                       await Where<TEntity, TId, TKey>(base.Query<TEntity, TId>(), search, page, perPage, orderBy, orderbyDirection, track);


        public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            await Task.CompletedTask;

            if (!track)
                entities = entities.AsNoTracking();

            entities = entities.Where(search);


            var data = default(IEnumerable<TEntity>);

            if (orderBy != null)
                data = entities.OrderBy(orderBy, orderbyDirection);

            if (page != null)
            {
                PaginationUtils.Paginate(out int skip, perPage, page);
                data = data.Skip(skip).Take(perPage).ToList();
            }

            return data.ToList();

        }






        public async Task<int> Count<TEntity, TId>(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Count<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), where);

        public async Task<int> Count<TEntity, TId>(Expression<Func<TEntity, bool>> where)
            where TId : class
            where TEntity : BaseEntity<TId>
                => await Count<TEntity, TId>(base.Query<TEntity, TId>(), where);


        public async Task<int> Count<TEntity, TId>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var result = await entities.Where(where).CountAsync();
            return result;
        }
        public async Task<int> Count<TEntity, TId>()
            where TId : class
            where TEntity : BaseEntity<TId>
        {
            var result = await base.Query<TEntity, TId>().CountAsync();
            return result;
        }









    }

}
