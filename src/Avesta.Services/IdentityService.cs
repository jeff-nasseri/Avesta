using AutoMapper;
using Avesta.Data.Model;
using Avesta.Model;
using Avesta.Model.Controller;
using Avesta.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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

        public Task<IEnumerable<TAvestaUser>> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
