using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Avesta.Share.Model.Attribute.Authorization;
using Avesta.Data.Entity.Model;
using Avesta.HTTP.Auth.Service;
using Microsoft.AspNetCore.Identity;
using Avesta.Share.Enum;
using Avesta.HTTP.JWT.Service;
using Avesta.Constant.Auth.JWT;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Avesta.Exceptions.Identity;
using Avesta.Data.IdentityCore.Model;

namespace Avesta.Attribute.Authorize
{


    public class AuthSPARequiredFilter<TAvestaUser, TRole> : IAuthorizationFilter
        where TAvestaUser : AvestaIdentityUser
        where TRole : IdentityRole
    {

        readonly AuthorizAttrubuteModel _authorizaAttrubuteModel;
        readonly IHttpAuthService<TAvestaUser> _authService;
        readonly IJWTAuthenticationService _jWTAuthenticationService;
        public AuthSPARequiredFilter(
            [FromServices] IHttpAuthService<TAvestaUser> httpAuthService
            , [FromServices] IJWTAuthenticationService jWTAuthenticationService
            , AuthorizAttrubuteModel authorizaAttrubuteModel)
        {
            _authorizaAttrubuteModel = authorizaAttrubuteModel;
            _jWTAuthenticationService = jWTAuthenticationService;
            _authService = httpAuthService;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {

            var token = await _authService.GetBearerTokenFromContext();

            var featuresStr = await _jWTAuthenticationService.GetClaimFromToken(token, ClaimKeys.AccessesFeatures);
            var features = JsonConvert.DeserializeObject<List<string>>(featuresStr);

            var counter = 0;
            foreach (var feature in features)
            {
                if (_authorizaAttrubuteModel.Features.Any(m => m == feature))
                    counter++;
            }

            if (counter < _authorizaAttrubuteModel.Features.Length)
                throw new AuhotizationAccessException("User does not have any access to current endpoint");
        }
    }

    public class AuthorizeAccessAttribute<TAvestaUser, TRole> : TypeFilterAttribute
        where TAvestaUser : AvestaIdentityUser
        where TRole : IdentityRole
    {
        public AuthorizeAccessAttribute(params object[] features) : base(typeof(AuthSPARequiredFilter<TAvestaUser, TRole>))
        {
            var result = new List<string>();
            foreach (var item in features)
            {
                result.Add((string)item);
            }
            Arguments = new object[] { new AuthorizAttrubuteModel { Features = result.ToArray() } };
        }
    }

}
