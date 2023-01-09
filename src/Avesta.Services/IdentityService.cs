using Avesta.Data.Model;
using Avesta.Share.Model.Controller;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avesta.Services
{

    [Obsolete("Not Implemented yet !")]
    public class IdentityService<TAvestaUser> : IBaseService<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        public Task<PaginationModel<TAvestaUser>> Paginate(int? page = null, int perPage = 7, string searchKeyWord = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> PaginateDynamicQuery(string navigationPropertyPath, string where, string select, string orderBy, int? page = null, int perpage = 7)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> PaginateDynamicQuery(string navigationPropertyPath, string where, string select, string orderBy, int? page = null, int? perpage = 7)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<dynamic>> PaginateDynamicQuery(string navigationPropertyPath, string where, string select, string orderBy, int? takeFromLast = null, int? page = null, int? perpage = 7)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationModel<TAvestaUser>> PaginateNavigationChildren(int? page = null, string navigation = null, bool? navigateAll = null, int perPage = 7, string searchKeyWord = null, string dynamicQuery = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> Search(string keyword, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }

        Task<PaginationDynamicModel> IBaseService<TAvestaUser>.PaginateDynamicQuery(string navigationPropertyPath, string where, string select, string orderBy, int? takeFromLast, int? page, int? perpage)
        {
            throw new NotImplementedException();
        }
    }
}
