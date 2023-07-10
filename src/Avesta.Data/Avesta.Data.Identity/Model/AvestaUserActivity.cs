using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Identity.Model
{
    public class AvestaUserActivity<TId, TAvestaUser> : BaseEntity<TId>
        where TId : class
        where TAvestaUser : IAvestaUser<TId>
    {
        public virtual string IPV4 { get; set; }
        public virtual string IPV6 { get; set; }
        public virtual string Agent { get; set; }
        public virtual string Referer { get; set; }
        public virtual string Host { get; set; }
        public virtual string Location { get; set; }
        public virtual string HitedAddress { get; set; }

        public TAvestaUser User { get; set; }
        [ForeignKey(nameof(User))]
        public TId UserId { get; set; }
    }

    

}
