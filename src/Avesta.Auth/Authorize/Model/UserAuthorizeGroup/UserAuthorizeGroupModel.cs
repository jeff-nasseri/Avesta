using Avesta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authorize.Model.UserAuthorizeGroup
{

    public class AvestaUserAuthorizeGroupModel : BaseModel
    {
        public virtual string UserId { get; set; }
        public virtual string GroupId { get; set; }
    }
}
