using Avesta.Attribute;
using Avesta.Storage.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Identity
{
    public class RegisterModelBase : IdentityModel
    {
        public virtual string RepeatPassword { get; set; }

        public string PhoneNumber { get; set; }

    }

}
