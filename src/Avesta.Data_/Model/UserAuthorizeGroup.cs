using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaUserAuthorizeGroup<TId> : BaseEntity<TId>
        where TId : class
    {
        public virtual TId GroupId { get; set; }

        public virtual TId UserId { get; set; }

    }
}
