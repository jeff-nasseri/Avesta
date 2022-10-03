using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaUserAuthorizeGroup : BaseEntity
    {
        public virtual string GroupId { get; set; }

        public virtual string UserId { get; set; }

    }
}
