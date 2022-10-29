using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Model.Controller;
using Avesta.Storage.Constant;

namespace Avesta.Services
{

    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> Search(string keyword);

        /// <summary>
        /// paginate entites
        /// </summary>
        /// <param name="page">number current of page</param>
        /// <param name="perPage">per page entity for show</param>
        /// <param name="searchKeyWord">search str for search in entites</param>
        /// <returns>tuple of entites and entites count</returns>
        Task<PaginationModel<T>> Paginate(int page, int perPage = Pagination.PerPage, string searchKeyWord = null);
        
    }


    public interface IBaseService<TModel,TViewModel> : IBaseService<TModel>
        where TModel : class 
        where TViewModel : class
    {
        Task<IEnumerable<TViewModel>> SearchAsViewModel(string keyword);

        /// <summary>
        /// paginate entites
        /// </summary>
        /// <param name="page">number current of page</param>
        /// <param name="perPage">per page entity for show</param>
        /// <param name="searchKeyWord">search str for search in entites</param>
        /// <returns>tuple of entites and entites count</returns>
        Task<PaginationModel<TViewModel>> PaginateAsViewModel(int page, int perPage = Pagination.PerPage, string searchKeyWord = null);

    }

}
