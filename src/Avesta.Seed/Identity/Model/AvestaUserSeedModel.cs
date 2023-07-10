using Avesta.Data.IdentityCore.Model;

namespace Avesta.Seed.Identity.Model
{

    public class AvestaUserSeedModel<TAvestaUser>
        where TAvestaUser : AvestaIdentityUser
    {
        public TAvestaUser User { get; set; }
        public string Password { get; set; }
    }
}
