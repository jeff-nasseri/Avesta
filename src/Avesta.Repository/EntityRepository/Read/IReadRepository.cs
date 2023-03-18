using Avesta.Data.Model;
using Avesta.Storage.Constant;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.EntityRepository.Read
{
    public interface IReadRepository<TEntity, TId>
        where TId : class
        where TEntity : BaseEntity<TId>
    {



        Task<TEntity> First(string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> First(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> First(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);







        Task<TEntity> First(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> First(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> First(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);







        Task<TEntity> Get(TId key
           , string navigationPropertyPath
           , bool track = true
           , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> Get(TId key
             , bool includeAllPath
             , bool track = true
             , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> Get(IQueryable<TEntity> entities
             , TId key
             , bool track = true
             , bool exceptionRaiseIfNotExist = false);








        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);



        Task<TEntity> Get(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);








        Task<IEnumerable<TEntity>> GetAll<TKey>(
            string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);


        Task<IEnumerable<TEntity>> GetAll<TKey>(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);


        Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);










        Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PerPage
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false);

        Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);



        Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);






        Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
           , string navigationPropertyPath
           , int? page = null
           , int perPage = Pagination.PerPage
           , Func<TEntity, TKey> orderBy = null
           , OrderByDirection orderbyDirection = OrderByDirection.Ascending
           , bool track = false);


        Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);


        Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);





        Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath);
        Task<int> Count(Expression<Func<TEntity, bool>> where);
        Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where);
        Task<int> Count();








    }

}
