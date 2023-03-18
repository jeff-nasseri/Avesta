using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Share.Model;
using Avesta.Share.Model.Controller;
using Avesta.Storage.Constant;

namespace Avesta.Services
{

    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> Search(string keyword, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// paginate entites
        /// </summary>
        /// <param name="page">number current of page</param>
        /// <param name="perPage">per page entity for show</param>
        /// <param name="searchKeyWord">search str for search in entites</param>
        /// <returns>tuple of entites and entites count</returns>
        Task<PaginationModel<T>> Paginate(int? page = null, int perPage = Pagination.PerPage, string searchKeyWord = null, DateTime? startDate = null, DateTime? endDate = null);

        Task<PaginationModel<T>> PaginateNavigationChildren(int? page = null, string? navigation = null, bool? navigateAll = null, int perPage = Pagination.PerPage
            , string searchKeyWord = null
            , string? dynamicQuery = null
            , DateTime? startDate = null
            , DateTime? endDate = null);




        Task<PaginationDynamicModel> PaginateDynamicQuery(string navigationPropertyPath
            , string where
            , string select
            , string orderBy
            , int? takeFromLast = null
            , int? page = null
            , int? perpage = Pagination.PerPage);




    }


    public interface IBaseService<TModel, TViewModel> : IBaseService<TModel>
        where TModel : class
        where TViewModel : class
    {
        Task<IEnumerable<TViewModel>> SearchAsViewModel(string keyword, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// paginate entites
        /// </summary>
        /// <param name="page">number current of page</param>
        /// <param name="perPage">per page entity for show</param>
        /// <param name="searchKeyWord">search str for search in entites</param>
        /// <returns>tuple of entites and entites count</returns>
        Task<PaginationModel<TViewModel>> PaginateAsViewModel(int? page = null, int perPage = Pagination.PerPage
            , string searchKeyWord = null
            , DateTime? startDate = null
            , DateTime? endDate = null);

    }







    public interface IEntityReadService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
    }


    public interface IEntityReadService<TId, TEntity, TModel, TCreateModel, TEditModel> : IEntityReadService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
        where TEditModel : TModel
    {
    }



    public interface IEntityService<TId, TEntity, TModel> : IEntityReadService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
    }


    public interface IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> : IEntityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
        where TEditModel : TModel
    { }





































}
