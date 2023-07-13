using Avesta.Data.Entity.Model;
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
    public class AvestaIdentityUser<TId> : IdentityUser<TId>, IAvestaUser<TId>
        where TId : class, IEquatable<TId>
    {
        public AvestaIdentityUser()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = CreatedDate;
        }

        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual DateTime? VerifiedDate { get; set; }
        public virtual string? ProfileImageUrl { get; set; }
        public virtual string? Address { get; set; }

        public virtual string? FirstName { get; set; }
        public virtual string? LastName { get; set; }

        [NotMapped]
        public string? FullName => $"{FirstName} {LastName}";

        public virtual string? IdentityNumber { get; set; }
        public bool IsLock { get; set; }



        #region [-JWT-]
        public virtual string? RefreshToken { get; set; }
        #endregion




    }
    public class AvestaIdentityUser<TId, TAvestaUserGroup> : AvestaIdentityUser<TId>
        where TId : class, IEquatable<TId>
        where TAvestaUserGroup : AvestaUserAuthorizeGroup<TId, AvestaIdentityUser<TId, TAvestaUserGroup>>
    {
        public virtual ICollection<TAvestaUserGroup>? UserAuthorizeGroups { get; set; }

    }


}
