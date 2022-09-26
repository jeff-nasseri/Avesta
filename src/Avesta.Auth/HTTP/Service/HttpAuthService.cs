using Avesta.Data.Model;
using Avesta.Repository.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Avesta.Auth.HTTP.Service
{
    public interface IHttpAuthService<TAvestaUser>
    {
        Task<TAvestaUser> GetCurrentLoggedUserByCookieAuth();
        Task<TAvestaUser> GetCurrentLoggedUserByJWTAuth();

    }

    public class HttpAuthService<TAvestaUser, TRole> : IHttpAuthService<TAvestaUser>
       where TAvestaUser : AvestaUser
       where TRole : IdentityRole
    {
        readonly IIdentityRepository<TAvestaUser, TRole> _identityRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
            public HttpAuthService(IHttpContextAccessor httpContextAccessor
            , IIdentityRepository<TAvestaUser, TRole> identityRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityRepository = identityRepository;
        }



        /// <summary>
        /// If user login with session or cookie authentication schema 
        /// Then you can get user from http context with this method
        /// </summary>
        /// <returns>Avesta User</returns>
        public async Task<TAvestaUser> GetCurrentLoggedUserByCookieAuth()
        {
            var claimPrinciple = _httpContextAccessor.HttpContext.User;
            var user = await _identityRepository.GetUser(claimPrinciple);
            return user;
        }



        /// <summary>
        /// If user login with jwt authentication schema 
        /// Then you can get user from http context with this method
        /// </summary>
        /// <returns>Avesta User</returns>
        public async Task<TAvestaUser> GetCurrentLoggedUserByJWTAuth()
        {
            throw new NotImplementedException();
        }
    }
}
