using Avesta.Data.Identity.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Share.Model;

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
    }

    public interface IUserManager<TUser, TId> : IUserActivityManager<TUser, TId>, IUserTokenManager<TUser, TId>
        where TId : class
        where TUser : AvestaUser<TId>
    {
        Task<IEnumerable<TUser>> Users { get; }
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
    public interface IUserTokenManager<TUser, TId>
        where TId : class
        where TUser : AvestaUser<TId>
    {
        Task<AvestaIdentityResult> SendConfirmationEmailToken(TUser user);
        Task<AvestaIdentityResult> SendConfirmationPhoneCode(TUser user);

        Task<AvestaIdentityResult> AddNewToken(TId userId, string name, string value);
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
        Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupName(TUser user, string groupName);
        Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupName(TUser user, string groupName);
        Task<AvestaIdentityResult> RemoveUserToExistingAuthorizeGroupByGroupId(TUser user, TId id);

        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupName(string groupName);
        Task<IEnumerable<TUser>> GetUsersOfAuthorizeGroupByGroupId(TId id);
    }


    public class AvestaUserHandler<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> : IUserManager<TAvestaUser, TId>, IUserAuthorizeGroupManager<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup, TId>
       where TId : class
       where TAvestaUser : AvestaUser<TId>
       where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
       where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
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

        public Task<IEnumerable<TAvestaUser>> Users => throw new NotImplementedException();

        public Task<AvestaIdentityResult> AddNewActivity(TAvestaUser user, string name)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddNewLoginActivity(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddNewLogOutActivity(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddNewResetPasswordActivity(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddNewToken(TId userId, string name, string value)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddNewUser(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TAvestaUser user, TId id)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupName(TAvestaUser user, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task<AvestaIdentityResult> BlockUser(TAvestaUser user, TimeSpan time)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> Find(Func<bool, TAvestaUser> single)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> FindByClaim(Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> FindById(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<TAvestaUser> FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TAvestaUser>> GetUsers(Func<bool, TAvestaUser> where)
        {
            throw new NotImplementedException();
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