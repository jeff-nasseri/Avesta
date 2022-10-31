using AutoMapper;
using Avesta.Data.Model;
using Avesta.Exceptions.Identity;
using Avesta.Repository.Identity;
using Avesta.Services;
using Avesta.Share.Extensions;
using Avesta.Share.Model.Identity;
using Microsoft.AspNetCore.Identity;

namespace Avesta.Auth.User.Service
{
    public interface IUserService<TAvestaUser> : IBaseService<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        Task<TAvestaUser> GetUserByEmail(string email, bool exceptionIfNotExist = false);
        Task<TAvestaUser> GetUserById(string id, bool exceptionIfNotExist = false);
        Task<TAvestaUser> Update<TUserModel>(TUserModel model) where TUserModel : UserBaseModel;
        Task Update(TAvestaUser user);
        Task<IEnumerable<TAvestaUser>> GetAll();
        Task<TAvestaUser> Delete(string id);
    }


    public class UserService<TAvestaUser, TRole> : IdentityService<TAvestaUser>, IUserService<TAvestaUser>
        where TAvestaUser : AvestaUser
        where TRole : IdentityRole
    {
        readonly IIdentityRepository<TAvestaUser, TRole> _identityRepository;
        readonly IMapper _mapper;

        public UserService(IIdentityRepository<TAvestaUser, TRole> identityRepository, IMapper mapper)
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

            var updatedUser = user.UpdateBy<TAvestaUser>(mappedUser);

            await _identityRepository.UpdateUser(updatedUser);
            return mappedUser;

        }

        public async Task<IEnumerable<TAvestaUser>> GetAll()
        {
            var users = await _identityRepository.GetUsers();
            return users;
        }


        public async Task<TAvestaUser> Delete(string id)
        {
            var user = await _identityRepository.GetUser(id);
            if (user == null)
                throw new UserNotFoundException(id);

            await _identityRepository.DeleteUser(user);

            return user;
        }

        public async Task Update(TAvestaUser user)
        {
            await _identityRepository.UpdateUser(user);
        }
    }
}
