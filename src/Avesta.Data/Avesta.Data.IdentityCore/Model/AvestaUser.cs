using Avesta.Data.Identity.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.IdentityCore.Model
{
    public class AvestaUser : IdentityUser
    {
        public AvestaUser()
        {
            RegisterDate = DateTime.UtcNow;
            ModifiedDate = RegisterDate;
        }

        public virtual DateTime RegisterDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual DateTime? VerifiedDate { get; set; }
        public virtual string? ProfileImageUrl { get; set; }
        public virtual string? Address { get; set; }

        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }

        [NotMapped]
        public string? FullName => $"{FirstName} {LastName}";

        public virtual string? IdentityNumber { get; set; }




        #region [-JWT-]
        public virtual string? RefreshToken { get; set; }

        #endregion




    }
    public class AvestaUser<TId, TAvestaUserGroup> : AvestaUser
        where TId : class
        where TAvestaUserGroup : AvestaUserAuthorizeGroup<TId>
    {
        public virtual ICollection<TAvestaUserGroup>? UserAuthorizeGroups { get; set; }

    }


}
