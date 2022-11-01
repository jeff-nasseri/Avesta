using Avesta.Data.Model;
using Avesta.HTTP.JWT.Service;
using Avesta.Repository.Identity;
using Avesta.Share.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Avesta.HTTP.Auth.Service
{
    public interface IHttpAuthService<TAvestaUser>
    {
        Task<TAvestaUser> GetCurrentLoggedUserByCookieAuth();
        Task<TAvestaUser> GetCurrentLoggedUserByJWTAuth();
        Task<TAvestaUser> GetCurrentLoggedUserFromContext();
        Task<string?> GetBearerTokenFromContext();
    }

    public class HttpAuthService<TAvestaUser, TRole> : IHttpAuthService<TAvestaUser>
       where TAvestaUser : AvestaUser
       where TRole : IdentityRole
    {
        readonly IIdentityRepository<TAvestaUser, TRole> _identityRepository;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IJWTAuthenticationService _jWTAuthenticationService;
        public HttpAuthService(IHttpContextAccessor httpContextAccessor
            , IIdentityRepository<TAvestaUser, TRole> identityRepository
            , IJWTAuthenticationService jWTAuthenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityRepository = identityRepository;
            _jWTAuthenticationService = jWTAuthenticationService;
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




        public async Task<TAvestaUser> GetCurrentLoggedUserFromContext()
        {
            throw new NotImplementedException();
        }


        public async Task<string?> GetBearerTokenFromContext()
        {
            var token = await _httpContextAccessor.HttpContext.GetHeaderValue("Authorization", prefix: "Bearer ");
            return token;
        }


    }
}
