using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.Identity
{
    public class RegisterModelBase<TId> : IdentityModel<TId>
        where TId : class, IEquatable<TId>
    {
        public virtual string RepeatPassword { get; set; }

        public string PhoneNumber { get; set; }

    }

}
