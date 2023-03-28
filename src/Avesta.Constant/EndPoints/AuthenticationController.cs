using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Constant.EndPoints
{
    public class AuthenticationController
    {
        public const string Register = "register";
        public const string Authenticate = "authenticate";
        public const string ReAuthenticate = "re-authenticate";
        public const string GetUserByJWTToken = "get-user-by-jwt-token";
        public const string SignOut = "signout";
    }
}
