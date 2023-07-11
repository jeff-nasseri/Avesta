using Avesta.Data.Identity.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Share.Model;
using System.Linq.Expressions;
using Z.Expressions;

namespace test
{
    public class Claim
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Provider { get; set; }
    }

    public class AvestaIdentityResult : ReturnTemplate<object>
    {
        public int IdentityStatus { get; set; }
        public string Message { get; set; }

        public static AvestaIdentityResult Ok(string message)
        {
            var newObj = new AvestaIdentityResult();
            newObj.IdentityStatus = 0;
            newObj.Succeed = true;
            newObj.Message = message;
            return newObj;
        }
        public static AvestaIdentityResult NotOk(string message)
        {
            var newObj = new AvestaIdentityResult();
            newObj.IdentityStatus = 500;
            newObj.Succeed = false;
            newObj.Message = message;
            return newObj;
        }
    }

    public interface IUserManager<TUser, TId, TAvestaUserToken> : IUserActivityManager<TUser, TId>, IUserTokenManager<TUser, TId, TAvestaUserToken>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaUserToken : AvestaUserToken<TId, TUser>
    {
        IEnumerable<TUser> Users { get; }
        Task<IEnumerable<TUser>> GetUsersByClaims(params Claim[] claims);
        Task<IEnumerable<TUser>> GetUsers(Func<bool, TUser> where);

        Task<TUser> FindById(TId id);
        Task<TUser> FindByEmail(string email);
        Task<TUser> FindByUserName(string username);
        Task<TUser> FindByClaim(Claim claim);
        Task<TUser> Find(Func<bool, TUser> single);

        Task<AvestaIdentityResult> AddNewUser(TUser user);
        Task<AvestaIdentityResult> ResetPassword(TUser user, string token, string newPassword);
        Task<AvestaIdentityResult> BlockUser(TUser user, TimeSpan time);

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
        Task<AvestaIdentityResult> SendConfirmationEmailToken(TUser user);
        Task<AvestaIdentityResult> SendConfirmationPhoneCode(TUser user);

        Task<AvestaIdentityResult> AddNewToken(TAvestaUserToken avestaUserToken);
    }
    public interface IAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId> : IUserAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        Task<AvestaIdentityResult> AddNewAuthorizeGroup(string name, IEnumerable<string> acess);
        Task<AvestaIdentityResult> DeleteAutorizeGroupByName(string name);
        Task<AvestaIdentityResult> DeleteAutorizeGroupById(TId id);
        Task<AvestaIdentityResult> DeleteAutorizeGroup(Func<bool, TAvestaAuthorizeGroup> single);
        Task<AvestaIdentityResult> UpdateAuthorizeGroup(TAvestaAuthorizeGroup group);

        Task<TAvestaAuthorizeGroup> GetAutorizeGroupByName(string name);
        Task<TAvestaAuthorizeGroup> GetAutorizeGroupById(TId id);
        Task<TAvestaAuthorizeGroup> GetAuthorizeGroup(Func<bool, TAvestaAuthorizeGroup> single);

        Task<IEnumerable<string>> GetAccessOfAuthorizeGroupByName(string name);
        Task<IEnumerable<string>> GetAccessOfAuthorizeGroupById(TId id);


    }
    public interface IUserAuthorizeGroupManager<TUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
        where TId : class
        where TUser : AvestaUser<TId>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupName(TUser user, string groupName);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);

        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupName(string groupName);
        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupId(TId id);
    }


    public class AvestaUserHandler<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TAvestaUserToken, TAvestaUserActivity> : IUserManager<TAvestaUser, TId>
        , IUserAuthorizeGroupManager<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
       where TId : class
       where TAvestaUser : AvestaUser<TId>
       where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
       where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
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

        public async Task<AvestaIdentityResult> AddNewActivity(TAvestaUser user, string name)
        {
            await _userActivitiesRepository.Insert(new AvestaUserActivity<TId, TAvestaUser>
            {
                User = user,
                Name = name
            });
            return AvestaIdentityResult.Ok("[Activity Added]");
        }

        public async Task<AvestaIdentityResult> AddNewLoginActivity(TAvestaUser user)
        {
            var resutl = await AddNewActivity(user, name: Activities.LOGIN);
            return resutl;
        }

        public async Task<AvestaIdentityResult> AddNewLogOutActivity(TAvestaUser user)
        {
            var result = await AddNewActivity(user, name: Activities.LOGOUT);
            return result;
        }

        public async Task<AvestaIdentityResult> AddNewResetPasswordActivity(TAvestaUser user)
        {
            var result = await AddNewActivity(user, name: Activities.RESET_PASSWORD);
            return result;
        }

        public async Task<AvestaIdentityResult> AddNewToken(TAvestaUserToken avestaUserToken)
        {
            await _userTokensRepository.Insert(avestaUserToken);
            return AvestaIdentityResult.Ok("[Token Added]");
        }

        public async Task<AvestaIdentityResult> AddNewUser(TAvestaUser user)
        {
            await _userRepository.Insert(user);
            return AvestaIdentityResult.Ok("[User Added]");
        }

        public async Task<AvestaIdentityResult> AddNewUserAuthorizeGroup(TUserAuthorizeGroup userAuthorizeGroup)
        {
            await _userAuthorizeGroupRepository.Insert(userAuthorizeGroup);
            return AvestaIdentityResult.Ok("[UserGroup Added]");
        }


        public async Task<AvestaIdentityResult> BlockUser(TId userId, TimeSpan time)
        {
            var user = await _userRepository.Get(userId, exceptionRaiseIfNotExist: true);
            user.LockoutEnd = DateTime.UtcNow + time;
            await _userRepository.Update(user);

            return AvestaIdentityResult.Ok($"[User Blocked Until {user.LockoutEnd?.ToString("yyyy/MM/dd HH:mm:ss")}]");
        }

        public async Task<TAvestaUser> Find(Expression<Func<TAvestaUser, bool>> single)
        {
            var result = await _userRepository.Get(predicate: single, exceptionRaiseIfNotExist: true);
            return result;
        }

        public async Task<TAvestaUser> FindByClaim(string name)
        {
            var result = await _userRepository.Get(predicate: u => u.Claims.Any(c => c.Name.ToLower() == name.ToLower())
            , navigationPropertyPath: $"{nameof(Claim)}"
            , exceptionRaiseIfNotExist: true);

            return result;
        }

        public async Task<TAvestaUser> FindByEmail(string email)
        {
            var user = await _userRepository.Get(u => u.Email == email, exceptionRaiseIfNotExist: true);
            return user;
        }

        public async Task<TAvestaUser> FindById(TId id)
        {
            var user = await _userRepository.Get(u => u.ID == id, exceptionRaiseIfNotExist: true);
            return user;
        }

        public async Task<TAvestaUser> FindByUserName(string username)
        {
            var user = await _userRepository.Get(u => u.Username == username, exceptionRaiseIfNotExist: true);
            return user;
        }

        public async Task<IEnumerable<TAvestaUser>> GetUsers(Expression<Func<TAvestaUser, bool>> where)
        {
            var result = await _userRepository.Where<DateTime>(search: where, includeAllPath: false
                , orderBy: u => u.CreatedDate
                , orderbyDirection: MoreLinq.OrderByDirection.Descending);

            return result;
        }

        public Task<IEnumerable<TAvestaUser>> GetUsersByClaims(params Claim[] claims)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> GetUsersOfAuthorizeGroupByGroupId(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> GetUsersOfAuthorizeGroupByGroupName(string groupName)
        {
            throw new NotImplementedException();
        }

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

        public Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupId(TAvestaUser user, TId id)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupName(TAvestaUser user, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> ResetPassword(TAvestaUser user, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> SendConfirmationEmailToken(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> SendConfirmationPhoneCode(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> UpdateEmail(TAvestaUser user, string token, string email)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> UpdateUser(TAvestaUser user, string token)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> UpdateUserName(TAvestaUser user, string token, string username)
        {
            throw new NotImplementedException();
        }
    }


}