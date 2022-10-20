using AutoMapper;
using Avesta.Auth.Authentication.ViewModel;
using Avesta.Data.Model;
using Avesta.Exceptions.Identity;
using Avesta.Model.Identity;
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
        Task<TAvestaUser> GetUserById(string id, bool exceptionIfNotExist = false);
        Task<TAvestaUser> Update<TUserModel>(TUserModel model) where TUserModel : UserBaseModel;
    }


    public class UserService<TAvestaUser, TRole> : IdentityService<TAvestaUser>, IUserService<TAvestaUser>
        where TAvestaUser : AvestaUser
        where TRole : IdentityRole
    {
        readonly IIdentityRepository<TAvestaUser, TRole> _identityRepository;
        readonly IMapper _mapper;

        public UserService(IIdentityRepository<TAvestaUser, TRole> identityRepository,IMapper mapper)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
        }

        public async Task<TAvestaUser> GetUserByEmail(string email, bool exceptionIfNotExist = false)
        {
            var user = await _identityRepository.GetUserByEmail(email);
            _ = exceptionIfNotExist ? (user == null ? throw new UserNotFoundException(email) : user) : user;
            return user;
        }

        public async Task<TAvestaUser> GetUserById(string id, bool exceptionIfNotExist = false)
        {
            var user = await _identityRepository.GetUser(id);
            _ = exceptionIfNotExist ? (user == null ? throw new UserNotFoundException(id) : user) : user;
            return user;
        }

        public async Task<TAvestaUser> Update<TUserModel>(TUserModel model) where TUserModel : UserBaseModel
        {
            var user = await _identityRepository.GetUser(model.ID);
            if (user == null)
                throw new UserNotFoundException(model.ID);

            var mappedUser = _mapper.Map<TAvestaUser>(model);
            await _identityRepository.UpdateUser(mappedUser);
            return mappedUser;

        }



    }
}
