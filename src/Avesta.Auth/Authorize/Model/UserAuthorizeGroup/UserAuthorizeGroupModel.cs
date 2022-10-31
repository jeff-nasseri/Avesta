using Avesta.Share.Model;

namespace Avesta.Auth.Authorize.Model.UserAuthorizeGroup
{

    public class AvestaUserAuthorizeGroupModel : BaseModel
    {
        public virtual string UserId { get; set; }
        public virtual string GroupId { get; set; }
    }
}
