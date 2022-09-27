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
    public class IdentityModel
    {
        public virtual string ID { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Username => Email;
    }
    public class LockOutCycle
    {
        public static DateTime Infinit = new DateTime(2999, 1, 1);
        public static DateTime OpenLock = new DateTime(1999, 1, 1);
    }
}
