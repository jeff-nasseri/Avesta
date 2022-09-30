using AutoMapper;
using Avesta.Data.Model;
using Avesta.Model;
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
    public class IdentityService<TAvestaUser> : IBaseService<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        public Task<(IEnumerable<TAvestaUser>, int)> Paginate(int page, int perPage = 7, string searchKeyWord = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> Search(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
