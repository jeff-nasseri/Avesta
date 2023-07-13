using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core.Tokenizer;

namespace Avesta.Auth.IdentityCore.Authentication.ViewModel
{
    public class JWTAvestaUser<TId> : AvestaIdentityUser<TId>
        where TId : class, IEquatable<TId>
    {
        public string? Token { get; set; }

        public JWTAvestaUser<TId>? SetToken(string token)
        {
            this.Token = token;
            return this;
        }
    }
}
