using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class UserAuthorizeGroup : BaseEntity
    {
        [ForeignKey(nameof(AuthorizeGroup))]
        public virtual string GroupId { get; set; }
        public virtual AvestaAuthorizeGroup AuthorizeGroup { get; set; }


        [ForeignKey(nameof(User))]
        public virtual string UserId { get; set; }
        public virtual AvestaUser User { get; set; }
    }
}
