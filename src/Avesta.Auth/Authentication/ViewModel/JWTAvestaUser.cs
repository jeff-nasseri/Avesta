using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authentication.ViewModel
{
    public class JWTAvestaUser : AvestaUser
    {
        public string? Token { get; set; }

        public JWTAvestaUser? SetToken(string token)
        {
            this.Token = token;
            return this;
        }
    }
}
