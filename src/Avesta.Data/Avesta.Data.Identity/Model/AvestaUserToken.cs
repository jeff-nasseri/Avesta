using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Identity.Model
{
    public class AvestaUserToken<TId, TAvestaUser> : BaseEntity<TId>
        where TId : class
        where TAvestaUser : IAvestaUser<TId>
    {
        public virtual string Value { get; set; }
        public virtual string Name { get; set; }



        public AvestaUserActivity<TId, TAvestaUser> Activity { get; set; }
        [ForeignKey(nameof(Activity))]
        public TId ActivityId { get; set; }


        [ForeignKey(nameof(User))]
        public TId UserId { get; set; }
        public TAvestaUser User { get; set; }
    }
}
