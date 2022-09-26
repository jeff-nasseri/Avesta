using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.JWT.Model
{
    public class JWTTokenIdentityResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
