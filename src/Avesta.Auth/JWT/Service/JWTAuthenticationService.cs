using Avesta.Auth.JWT.Model;
using Avesta.Data.Model;
using Avesta.Exceptions.Identity;
using Avesta.Repository.Identity;
using Avesta.Storage.Constant;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.JWT.Service
{

    public interface IJWTAuthenticationService
    {
        Task<string> GenerateRefreshToken();
        Task<JWTTokenIdentityResult> GenerateToken(IEnumerable<Claim> claims);
        Task<ClaimsPrincipal?> GetPrincipalFromToken(string? token);
        Task<JWTTokenIdentityResult> ReinitialIdentityJWTTokens(JWTTokenIdentityResult model);
        Task<string?> GetClaimFromToken(string token, string claimUrl);

    }
    public class JWTAuthenticationService<TAvestaUser, TRole> : IJWTAuthenticationService
        where TAvestaUser : AvestaUser
        where TRole : IdentityRole
    {

        readonly IConfiguration _configuration;
        IIdentityRepository<TAvestaUser, TRole> _identityRepository;

        public JWTAuthenticationService(IConfiguration configuration
            , IIdentityRepository<TAvestaUser, TRole> identityRepository)
        {
            _configuration = configuration;
            _identityRepository = identityRepository;
        }

        public async Task<string> GenerateRefreshToken()
        {
            await Task.CompletedTask;

            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public async Task<JWTTokenIdentityResult> ReinitialIdentityJWTTokens(JWTTokenIdentityResult model)
        {
            var principle = await GetPrincipalFromToken(model.Token);
            if (principle == null)
                throw new IdentityException(msg: "can not found principle in ReinitialIdentityJWTTokens", ExceptionConstant.IdentityException);


            var email = await GetClaimFromToken(model.Token, ClaimTypes.Email);

            if (email == null)
                throw new IdentityException(msg: "can not found email of principle", ExceptionConstant.IdentityException);

            var user = await _identityRepository.GetUserByEmail(email);

            if (user.RefreshToken != model.RefreshToken)
                throw new IdentityException(msg: "refresh token not the same!(somebody try to hack_us. be carefull)", ExceptionConstant.IdentityException);


            var identityToken = await GenerateToken(principle.Claims.ToList());
            return identityToken;
        }



        public async Task<string?> GetClaimFromToken(string token, string claimUrl)
        {
            var principle = await GetPrincipalFromToken(token);
            if (principle == null)
                throw new IdentityException(msg: "can not found principle in ReinitialIdentityJWTTokens", ExceptionConstant.IdentityException);

            var value = principle.Claims.SingleOrDefault(c => c.Type == claimUrl)?.Value;

            return value;
        }



        public async Task<JWTTokenIdentityResult> GenerateToken(IEnumerable<Claim> claims)
        {
            await Task.CompletedTask;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new JWTTokenIdentityResult
            {
                Token = tokenHandler.WriteToken(token),
                RefreshToken = await GenerateRefreshToken()
            };
        }

        public async Task<ClaimsPrincipal?> GetPrincipalFromToken(string? token)
        {
            await Task.CompletedTask;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new IdentityException(msg: "invalid token", ExceptionConstant.IdentityException);

            return principal;

        }
    }
}
