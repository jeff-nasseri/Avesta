using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Identity.Model
{
    public class AvestaUserAuthorizeGroup<TId, TAvestaUser> : BaseEntity<TId>
        where TId : class
        where TAvestaUser : IAvestaUser<TId>
    {
        public virtual TId GroupId { get; set; }

        [ForeignKey(nameof(User))]
        public virtual TId UserId { get; set; }
        public TAvestaUser User { get; set; }

    }
}
