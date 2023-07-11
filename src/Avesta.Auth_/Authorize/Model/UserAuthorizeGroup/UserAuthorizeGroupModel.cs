using Avesta.Share.Model;

namespace Avesta.Auth.Authorize.Model.UserAuthorizeGroup
{

    public class AvestaUserAuthorizeGroupModel<TId> : BaseModel<TId>
        where TId : class
    {
        public virtual TId UserId { get; set; }
        public virtual TId GroupId { get; set; }
    }
}
