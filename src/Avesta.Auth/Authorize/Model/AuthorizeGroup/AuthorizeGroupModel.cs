using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authorize.Model.AuthorizeGroup
{
    public class AuthorizeGroupModel : BaseModel
    {
        public virtual string GroupName { get; set; }
        public virtual IEnumerable<int> Features { get; set; }
    }
}
