using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.Identity
{
    public class LoginModelBase : IdentityModel
    {
        public virtual bool RememberMe { get; set; } = true;
    }
}
