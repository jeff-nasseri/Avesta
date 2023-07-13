using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.Identity
{
    public class LoginModelBase<TId> : IdentityModel<TId>
        where TId : class, IEquatable<TId>
    {
        public virtual bool RememberMe { get; set; } = true;
    }
}
