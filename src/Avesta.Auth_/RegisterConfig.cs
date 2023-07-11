using Avesta.Auth.Authentication.Config;
using Avesta.Auth.Authentication.Service;
using Avesta.Auth.Authorize.Service;
using Avesta.Auth.User.Service;
using Avesta.Data.Identity.Model;
using Avesta.Data.IdentityCore.Model;
using Avesta.HTTP.Auth.Service;
using Avesta.HTTP.JWT.Model;
using Avesta.HTTP.JWT.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Avesta.Data.Entity.Context;
using Avesta.Repository.IdentityCore;
using Avesta.Repository.Entity;
using Avesta.Repository.EntityRepository;
using Avesta.Share.Model;

namespace Avesta.Auth
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
    }


    public static class RegisterConfig
    {
        public static IServiceCollection AddAvestaAuthentication<TAvestaUser, TRole>(
            this IServiceCollection service
            , Action<AvestaAuthenticationOption> setupActionForAvestaAuth)
            where TAvestaUser : AvestaIdentityUser
            where TRole : IdentityRole
        {

            #region [-Register JWTAuth-]
            service.AddScoped<IJWTAuthenticationService, JWTAuthenticationService<TAvestaUser, TRole>>();
            service.AddScoped<IIdentityRepository<TAvestaUser, TRole>, IdentityRepository<TAvestaUser, TRole>>();
            var option = new AvestaAuthenticationOption();
            setupActionForAvestaAuth(option);
            service.AddSingleton(option);
            #endregion


            #region [-Register HttpAuth-]
            service.AddScoped<IHttpAuthService<TAvestaUser>, HttpAuthService<TAvestaUser, TRole>>();
            #endregion


            #region [-Register Authentication-]
            service.AddScoped<IAuthenticationService<TAvestaUser>, AuthenticationService<TAvestaUser, TRole>>();
            #endregion


            #region [-Configure AutoMapper-]
            service.ConfigureMapperForAuthentication<TAvestaUser>();
            #endregion



            #region [-Register User-]
            service.AddScoped<IUserService<TAvestaUser>, UserService<TAvestaUser, TRole>>();
            #endregion

            return service;
        }





        public static IServiceCollection AddAvestaAuthorization<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup, TAvestaDbContext>(this IServiceCollection service)
            where TId : class
            where TAvestaUser : AvestaUser<TId, TAvestaUserAuthorizeGroup>
            where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TAvestaUserAuthorizeGroup>
            where TAvestaDbContext : AvestaDbContext
            where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
        {

            service.RegisterRepositories<TId, TAvestaAuthorizeGroup, TAvestaDbContext>();
            service.RegisterRepositories<TId, TAvestaUserAuthorizeGroup, TAvestaDbContext>();


            service.AddScoped<IAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            service.AddScoped<IAvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
                , AvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>>();

            return service;
        }




    }


}
