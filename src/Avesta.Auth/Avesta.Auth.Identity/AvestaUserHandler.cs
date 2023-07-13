using AutoMapper;
using Avesta.Auth.Identity.Model;
using Avesta.Data.Identity.Model;
using Avesta.Repository.EntityRepository;
using System.Linq.Expressions;

namespace Avesta.Auth.Identity
{




    public interface IUserManager<TUser, TId, TAvestaUserToken> : IUserActivityManager<TUser, TId>, IUserTokenManager<TUser, TId, TAvestaUserToken>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaUserToken : AvestaUserToken<TId, TUser>
    {
        IEnumerable<TUser> Users { get; }
        Task<IEnumerable<TUser>> GetUsersByClaims(params Claim[] claims);
        Task<IEnumerable<TUser>> GetUsers(Expression<Func<TUser, bool>> where);

        Task<TUser> FindById(TId id);
        Task<TUser> FindByEmail(string email);
        Task<TUser> FindByUserName(string username);
        Task<TUser> FindByClaim(string name);
        Task<TUser> Find(Expression<Func<TUser, bool>> single);

        Task<AvestaIdentityResult> AddNewUser(TUser user);
        Task<AvestaIdentityResult> ResetPassword(TUser user, string token, string password, string newPassword);
        Task<AvestaIdentityResult> BlockUser(TId id, TimeSpan time);

        Task<TUser> UpdateUser(TUser user, string token);
        Task<TUser> UpdateEmail(TUser user, string token, string email);
        Task<TUser> UpdateUserName(TUser user, string token, string username);

        Task<AvestaIdentityResult> LoginByEmail(string email, string password);
        Task<AvestaIdentityResult> LoginByUserName(string username, string password);
        Task<AvestaIdentityResult> LoginByDisposableToken(string token);
        Task<AvestaIdentityResult> LogOut(TUser user);


    }
    public interface IUserActivityManager<TUser, TId>
        where TId : class
        where TUser : AvestaUser<TId>
    {
        Task<AvestaIdentityResult> AddNewActivity(TUser user, string name);
        Task<AvestaIdentityResult> AddNewLoginActivity(TUser user);
        Task<AvestaIdentityResult> AddNewLogOutActivity(TUser user);
        Task<AvestaIdentityResult> AddNewResetPasswordActivity(TUser user);
    }
    public interface IUserTokenManager<TUser, TId, TAvestaUserToken>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaUserToken : AvestaUserToken<TId, TUser>
    {
    }
    public interface IAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId> : IUserAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TUser>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TUser>
    {
        Task<AvestaIdentityResult> AddNewAuthorizeGroup(string name, IEnumerable<string> acess);
        Task<AvestaIdentityResult> DeleteAutorizeGroupByName(string name);
        Task<AvestaIdentityResult> DeleteAutorizeGroupById(TId id);
        Task<AvestaIdentityResult> DeleteAutorizeGroup(Expression<Func<TAvestaAuthorizeGroup, bool>> single);
        Task<AvestaIdentityResult> UpdateAuthorizeGroup(TAvestaAuthorizeGroup group);

        Task<TAvestaAuthorizeGroup> GetAutorizeGroupByName(string name);
        Task<TAvestaAuthorizeGroup> GetAutorizeGroupById(TId id);
        Task<TAvestaAuthorizeGroup> GetAuthorizeGroup(Expression<Func<TAvestaAuthorizeGroup, bool>> single);

    }
    public interface IUserAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TUser>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TUser>
    {
        Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupName(TUser user, string groupName);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);

        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupName(string groupName);
        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupId(TId id);
    }

    public abstract class AvestaUserHandler<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TAvestaUserToken, TAvestaUserActivity> : IUserManager<TAvestaUser, TId, TAvestaUserToken>
        , IAuthorizeGroupManager<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
       where TId : class
       where TAvestaUser : AvestaUser<TId>
       where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TAvestaUser>
       where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId, TAvestaUser>
       where TAvestaUserToken : AvestaUserToken<TId, TAvestaUser>
       where TAvestaUserActivity : AvestaUserActivity<TId, TAvestaUser>
    {
        readonly IEntityRepository<TAvestaUser, TId> _userRepository;
        readonly IEntityRepository<TAvestaAuthorizeGroup, TId> _authorizeGroupRepository;
        readonly IEntityRepository<TUserAuthorizeGroup, TId> _userAuthorizeGroupRepository;
        readonly IEntityRepository<AvestaUserActivity<TId, TAvestaUser>, TId> _userActivitiesRepository;
        readonly IEntityRepository<AvestaUserToken<TId, TAvestaUser>, TId> _userTokensRepository;

        public AvestaUserHandler(IEntityRepository<TAvestaUser, TId> userRepository
            , IEntityRepository<TAvestaAuthorizeGroup, TId> authorizeGroupRepository
            , IEntityRepository<TUserAuthorizeGroup, TId> userAuthorizeGroupRepository
            , IEntityRepository<AvestaUserActivity<TId, TAvestaUser>, TId> userActivitiesRepository
            , IEntityRepository<AvestaUserToken<TId, TAvestaUser>, TId> userTokensRepository)
        {
            _userRepository = userRepository;
            _authorizeGroupRepository = authorizeGroupRepository;
            _userAuthorizeGroupRepository = userAuthorizeGroupRepository;
            _userActivitiesRepository = userActivitiesRepository;
            _userTokensRepository = userTokensRepository;
        }

        public IEnumerable<TAvestaUser> Users => _userRepository.GetAll().Result;



        #region [- Activity -]
        public virtual async Task<AvestaIdentityResult> AddNewActivity(TAvestaUser user, string name)
        {
            await _userActivitiesRepository.Insert(new AvestaUserActivity<TId, TAvestaUser>
            {
                User = user,
                Name = name
            });
            return AvestaIdentityResult.Ok("[Activity Added]");
        }

        public virtual async Task<AvestaIdentityResult> AddNewLoginActivity(TAvestaUser user)
        {
            var resutl = await AddNewActivity(user, name: Activities.LOGIN);
            return resutl;
        }

        public virtual async Task<AvestaIdentityResult> AddNewLogOutActivity(TAvestaUser user)
        {
            var result = await AddNewActivity(user, name: Activities.LOGOUT);
            return result;
        }

        public virtual async Task<AvestaIdentityResult> AddNewResetPasswordActivity(TAvestaUser user)
        {
            var result = await AddNewActivity(user, name: Activities.RESET_PASSWORD);
            return result;
        }
        #endregion



        #region [- Token -]
        public virtual async Task<AvestaIdentityResult> AddNewToken(TAvestaUserToken avestaUserToken)
        {
            await _userTokensRepository.Insert(avestaUserToken);
            return AvestaIdentityResult.Ok("[Token Added]");
        }

        public abstract Task<TAvestaUserToken> GenerateToken(TAvestaUser user);
        public abstract Task<string> GenerateToken();

        #endregion


        #region [- User -]
        public virtual async Task<AvestaIdentityResult> AddNewUser(TAvestaUser user)
        {
            await _userRepository.Insert(user);
            return AvestaIdentityResult.Ok("[User Added]");
        }

        public virtual async Task<AvestaIdentityResult> AddNewUserAuthorizeGroup(TUserAuthorizeGroup userAuthorizeGroup)
        {
            await _userAuthorizeGroupRepository.Insert(userAuthorizeGroup);
            return AvestaIdentityResult.Ok("[UserGroup Added]");
        }


        public virtual async Task<AvestaIdentityResult> BlockUser(TId userId, TimeSpan time)
        {
            var user = await _userRepository.Get(userId, exceptionRaiseIfNotExist: true);
            user.LockoutEnd = DateTime.UtcNow + time;
            await _userRepository.Update(user);

            return AvestaIdentityResult.Ok($"[User Blocked Until {user.LockoutEnd?.ToString("yyyy/MM/dd HH:mm:ss")}]");
        }

        public virtual async Task<TAvestaUser> Find(Expression<Func<TAvestaUser, bool>> single)
        {
            var result = await _userRepository.Get(predicate: single, exceptionRaiseIfNotExist: true);
            return result;
        }

        public virtual async Task<TAvestaUser> FindByClaim(string name)
        {
            var result = await _userRepository.Get(predicate: u => u.Claims.Any(c => c.Name.ToLower() == name.ToLower())
            , navigationPropertyPath: $"{nameof(Claim)}"
            , exceptionRaiseIfNotExist: true);

            return result;
        }

        public virtual async Task<TAvestaUser> FindByEmail(string email)
        {
            var user = await _userRepository.Get(u => u.Email == email, exceptionRaiseIfNotExist: true);
            return user;
        }

        public virtual async Task<TAvestaUser> FindById(TId id)
        {
            var user = await _userRepository.Get(u => u.Id == id, exceptionRaiseIfNotExist: true);
            return user;
        }

        public virtual async Task<TAvestaUser> FindByUserName(string username)
        {
            var user = await _userRepository.Get(u => u.UserName == username, exceptionRaiseIfNotExist: true);
            return user;
        }

        public virtual async Task<IEnumerable<TAvestaUser>> GetUsers(Expression<Func<TAvestaUser, bool>> where)
        {
            var result = await _userRepository.Where<DateTime>(search: where, includeAllPath: false
                , orderBy: u => u.CreatedDate
                , orderbyDirection: MoreLinq.OrderByDirection.Descending);

            return result;
        }

        public virtual async Task<IEnumerable<TAvestaUser>> GetUsersByClaims(params Claim[] claims)
        {
            var result = await _userRepository.Where<DateTime>(search: u => u.Claims.Any(c => claims.Any(cl => cl.Name.ToLower() == c.Name.ToLower()))
            , navigationPropertyPath: nameof(Claim));

            return result;
        }
        #endregion


        #region [- Authorize Group -]
        public virtual async Task<IEnumerable<TAvestaUser>> GetUsersOfAuthorizeGroupByGroupId(TId id)
        {
            var group = await _authorizeGroupRepository.Get(id, navigationPropertyPath: nameof(AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TAvestaUser>.UserAuthorizeGroups)
                , exceptionRaiseIfNotExist: true);
            var users = group?.UserAuthorizeGroups?.Select(uag => uag.User).ToList();

            return users;
        }

        public virtual async Task<IEnumerable<TAvestaUser>> GetUsersOfAuthorizeGroupByGroupName(string groupName)
        {
            var group = await _authorizeGroupRepository.Get(g => g.GroupName.ToLower() == groupName.ToLower()
                , navigationPropertyPath: nameof(AvestaAuthorizeGroup<TId, TUserAuthorizeGroup, TAvestaUser>.UserAuthorizeGroups)
                , exceptionRaiseIfNotExist: true);
            var users = group?.UserAuthorizeGroups?.Select(uag => uag.User).ToList();

            return users;
        }
        public virtual async Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupId(TAvestaUser user, TId id)
        {
            await _userAuthorizeGroupRepository.Delete(uag => uag.UserId == user.Id && uag.GroupId == id);
            return AvestaIdentityResult.Ok($"[User Removed From Group {id}]");
        }

        public virtual async Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupName(TAvestaUser user, string groupName)
        {
            var group = await _authorizeGroupRepository.Get(g => g.GroupName.ToLower() == groupName.ToLower(), exceptionRaiseIfNotExist: true);
            await _userAuthorizeGroupRepository.Delete(uag => uag.UserId == user.Id && uag.GroupId == group.Id);
            return AvestaIdentityResult.Ok($"[User Removed From Group {group.GroupName}]");
        }

        public abstract Task<AvestaIdentityResult> AddNewAuthorizeGroup(string name, IEnumerable<string> acess);
        public virtual async Task<AvestaIdentityResult> AddNewAuthorizeGroup(TAvestaAuthorizeGroup avestaAuthorizeGroup)
        {
            await _authorizeGroupRepository.Insert(avestaAuthorizeGroup);
            return AvestaIdentityResult.Ok("[Authorize Group Added]");
        }

        public virtual async Task<AvestaIdentityResult> DeleteAutorizeGroupByName(string name)
        {
            await _authorizeGroupRepository.Delete(g => g.GroupName.ToLower() == name.ToLower(), exceptionRaiseIfNotExist: true);
            return AvestaIdentityResult.Ok("Authorize Group Removed");
        }

        public virtual async Task<AvestaIdentityResult> DeleteAutorizeGroupById(TId id)
        {
            await _authorizeGroupRepository.Delete(id, exceptionRaiseIfNotExist: true);
            return AvestaIdentityResult.Ok("Authorize Group Removed");
        }

        public virtual async Task<AvestaIdentityResult> DeleteAutorizeGroup(Expression<Func<TAvestaAuthorizeGroup, bool>> single)
        {
            await _authorizeGroupRepository.Delete(single: single, exceptionRaiseIfNotExist: true);
            return AvestaIdentityResult.Ok("Authorize Group Removed");
        }

        public Task<AvestaIdentityResult> UpdateAuthorizeGroup(TAvestaAuthorizeGroup group)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TAvestaAuthorizeGroup> GetAutorizeGroupByName(string name)
        {
            var result = await _authorizeGroupRepository.Get(g => g.GroupName.ToLower() == name.ToLower(), exceptionRaiseIfNotExist: true);
            return result;
        }

        public virtual async Task<TAvestaAuthorizeGroup> GetAutorizeGroupById(TId id)
        {
            var result = await _authorizeGroupRepository.Get(id, exceptionRaiseIfNotExist: true);
            return result;
        }

        public virtual async Task<TAvestaAuthorizeGroup> GetAuthorizeGroup(Expression<Func<TAvestaAuthorizeGroup, bool>> single)
        {
            var result = await _authorizeGroupRepository.Get(single, exceptionRaiseIfNotExist: true);
            return result;
        }

        public abstract Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TAvestaUser user, TId id);


        #endregion

        #region [- Log(in/out) -]

        public Task<AvestaIdentityResult> LoginByDisposableToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> LoginByEmail(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> LoginByUserName(string username, string password)
        {
            throw new NotImplementedException();
        }
        public Task<AvestaIdentityResult> LogOut(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        #endregion




        #region [- Profile -]
        public virtual async Task<AvestaIdentityResult> ResetPassword(TAvestaUser user, string token, string password, string newPassword)
        {
            await _userTokensRepository.CheckAvailability(t => t.Value == token && t.Name == TokenProvider.RESET_PASSWORD);
            var target = await _userRepository.Get(u => u.Id == user.Id, exceptionRaiseIfNotExist: true);
            if (!target.ValidatePassword(password))
                throw new Exception("Password not match");

            target.SetPassword(newPassword);
            await _userRepository.Update(target);

            return AvestaIdentityResult.Ok("[Password Updated]");
        }


        public virtual async Task<TAvestaUser> UpdateEmail(TAvestaUser user, string token, string email)
        {
            await _userTokensRepository.CheckAvailability(t => t.Value == token && t.Name == TokenProvider.UPDATE_EMAIL);
            var target = await _userRepository.Get(u => u.Id == user.Id, exceptionRaiseIfNotExist: true);
            target.Email = email;
            await _userRepository.Update(target);
            return target;
        }

        public Task<TAvestaUser> UpdateUser(TAvestaUser user, string token)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TAvestaUser> UpdateUserName(TAvestaUser user, string token, string username)
        {
            await _userTokensRepository.CheckAvailability(t => t.Value == token && t.Name == TokenProvider.UPDATE_USERNAME);
            var target = await _userRepository.Get(u => u.Id == user.Id, exceptionRaiseIfNotExist: true);
            target.Email = username;
            await _userRepository.Update(target);
            return target;
        }


        #endregion

    }






}