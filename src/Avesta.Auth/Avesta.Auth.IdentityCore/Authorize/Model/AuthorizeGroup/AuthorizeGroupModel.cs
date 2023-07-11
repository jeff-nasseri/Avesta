using Avesta.Share.Model;

namespace Avesta.Auth.IdentityCore.Authorize.Model.AuthorizeGroup
{
    public class AvestaAuthorizeGroupModel<TId> : BaseModel<TId>
        where TId : class
    {
        public virtual string GroupName { get; set; }
        public virtual string? AccessStr { get; set; }
    }
}
