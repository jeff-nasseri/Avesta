using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Task<(IEnumerable<T>, int)> Paginate(int page, int perPage = Pagination.PerPage, string searchKeyWord = null);
    }
}
