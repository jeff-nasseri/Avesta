using Avesta.Data.IdentityCore.Model;

namespace Avesta.Seed.Identity.Model
{

    public class AvestaUserSeedModel<TId, TAvestaUser>
        where TId : class, IEquatable<TId>
        where TAvestaUser : AvestaIdentityUser<TId>
    {
        public TAvestaUser User { get; set; }
        public string Password { get; set; }
    }
}
