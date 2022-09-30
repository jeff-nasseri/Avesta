using Avesta.Data.Model;
using Avesta.Exceptions.Identity;
using Avesta.Repository.Identity;
using Avesta.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.User.Service
{
    public interface IUserService<TAvestaUser> : IBaseService<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        Task<TAvestaUser> GetUserByEmail(string email, bool exceptionIfNotExist = false);
    }


    public class UserService<TAestaUser, TRole> : IdentityService<TAestaUser>, IUserService<TAestaUser>
        where TAestaUser : AvestaUser
        where TRole : IdentityRole
    {
        readonly IIdentityRepository<TAestaUser, TRole> _identityRepository;

        public UserService(IIdentityRepository<TAestaUser, TRole> identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<TAestaUser> GetUserByEmail(string email, bool exceptionIfNotExist = false)
        {
            var user = await _identityRepository.GetUserByEmail(email);
            _ = exceptionIfNotExist ? (user == null ? throw new UserNotFoundException(email) : user) : user;
            return user;
        }
    }
}
