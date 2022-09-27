using Avesta.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed.Identity.Model
{

    public class AvestaUserSeedModel<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        public TAvestaUser User { get; set; }
        public string Password { get; set; }
    }
}
