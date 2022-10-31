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
        public Task<PaginationModel<TAvestaUser>> Paginate(int page, int perPage = 7, string searchKeyWord = null)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationModel<TAvestaUser>> PaginateNavigationChildren(int page, string navigation = null, bool? navigateAll = null, int perPage = 7, string searchKeyWord = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
