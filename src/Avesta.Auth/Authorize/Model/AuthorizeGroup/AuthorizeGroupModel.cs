using Avesta.Share.Model;

namespace Avesta.Auth.Authorize.Model.AuthorizeGroup
{
    public class AvestaAuthorizeGroupModel : BaseModel
    {
        public virtual string GroupName { get; set; }
        public virtual IEnumerable<int> Features { get; set; }
    }
}
