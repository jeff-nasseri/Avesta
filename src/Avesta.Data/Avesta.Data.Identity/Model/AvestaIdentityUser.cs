using Avesta.Data.Entity.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Identity.Model
{

    public class BaseIdentity<TId> : BaseEntity<TId> where TId : class
    {
    }
    public class BaseIdentity : BaseIdentity<string> { }




    public class AvestaIdentityUser : AvestaIdentityUser<string>
    {
    }

    public partial class AvestaIdentityUser<TId> : BaseIdentity<TId> where TId : class, IEquatable<TId>
    {
        public AvestaIdentityUser() : base()
        {
        }
        public virtual string? Username { get; set; }
        public virtual string? NormalizedUserName { get; set; }
        public virtual string? Email { get; set; }
        public virtual bool? EmailConfirmed { get; set; }
        public virtual string NormalizedEmail { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecuritySalt { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public virtual DateTimeOffset? LockoutEnd { get; set; }



        public override string ToString()
        {
            var result = $"{Username}-{Email}";
            return result;
        }

        public override bool Equals(object? obj)
        {
            if (obj is AvestaIdentityUser<TId>)
            {
                var u1 = obj as AvestaIdentityUser<TId>;
                _ = u1 ?? throw new ArgumentNullException(nameof(obj));
                return (u1.Username == this.Username && u1.NormalizedUserName == this.NormalizedUserName && u1.PasswordHash == this.PasswordHash && u1.Email == this.Email)
                    || u1.ID == this.ID;
            }
            return false;
        }



    }
}
