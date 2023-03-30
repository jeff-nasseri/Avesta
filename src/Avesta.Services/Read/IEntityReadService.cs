using Avesta.Data.Model;
using Avesta.Share.Model;
using Avesta.Constant;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Avesta.Share.Signature;

namespace Avesta.Services.Read
{

    public interface IReadEntityService<TId, TEntity, TModel> : ISearchable<IEnumerable<TModel>>
           where TId : class
           where TEntity : BaseEntity<TId>
           where TModel : BaseModel<TId>
    {


        Task<TModel> First(string navigationPropertyPath
           , bool track = true
           , bool exceptionRaiseIfNotExist = false);

        Task<TModel> First(bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TModel> First(IQueryable<TEntity> entities
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);





        Task<TModel> First(Expression<Func<TEntity, bool>> search
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);
        Task<TModel> First(Expression<Func<TEntity, bool>> search
            , bool includeAllPath = false
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TModel> First(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> search
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);







        Task<TModel> Get(TId key
           , string navigationPropertyPath
           , bool track = true
           , bool exceptionRaiseIfNotExist = false);
        Task<TModel> Get(TId key
             , bool includeAllPath
             , bool track = true
             , bool exceptionRaiseIfNotExist = false);

        Task<TModel> Get(IQueryable<TEntity> entities
             , TId key
             , bool track = true
             , bool exceptionRaiseIfNotExist = false);






        Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , string navigationPropertyPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TModel> Get(Expression<Func<TEntity, bool>> predicate
            , bool includeAllPath
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);

        Task<TModel> Get(IQueryable<TEntity> entities
            , Expression<Func<TEntity, bool>> predicate
            , bool track = true
            , bool exceptionRaiseIfNotExist = false);









        Task<IEnumerable<TModel>> GetAll<TKey>(
            string navigationPropertyPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null);

        Task<IEnumerable<TModel>> GetAll<TKey>(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null);

        Task<IEnumerable<TModel>> GetAll(
             bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , bool track = false
            , object[] keywords = null);


        Task<IEnumerable<TModel>> GetAll<TKey>(IQueryable<TEntity> entities
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false
            , object[] keywords = null);







        Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PerPage
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false);

        Task<IEnumerable<TModel>> GetByIds<TKey>(IEnumerable<TId> ids
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);


        Task<IEnumerable<TModel>> GetByIds<TKey>(IQueryable<TEntity> entities
            , IEnumerable<TId> ids
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);







        Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
           , string navigationPropertyPath
           , int? page = null
           , int perPage = Pagination.PerPage
           , Func<TEntity, TKey> orderBy = null
           , OrderByDirection orderbyDirection = OrderByDirection.Ascending
           , bool track = false);


        Task<IEnumerable<TModel>> Where<TKey>(Expression<Func<TEntity, bool>> search
            , bool includeAllPath
            , int? page = null
            , int perPage = Pagination.PerPage
            , Func<TEntity, TKey> orderBy = null
            , OrderByDirection orderbyDirection = OrderByDirection.Ascending
            , bool track = false);

        Task<IEnumerable<TModel>> Where<TKey>(IQueryable<TEntity> entities
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
