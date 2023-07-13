using Avesta.Data.Entity.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Avesta.Security.Hash.Extension;

namespace Avesta.Data.Identity.Model
{

    public static class Activities
    {
        public const string LOGIN = "Login";
        public const string LOGOUT = "Logout";
        public const string RESET_PASSWORD = "ResetPassword";
    }

    public static class TokenProvider
    {
        public const string RESET_PASSWORD = "rest_password";
        public const string UPDATE_EMAIL = "update_email";
        public const string UPDATE_USERNAME = "update_username";
    }


    public class AvestaUser : AvestaUser<string>
    {
    }



    public interface IAvestaUser<TId> : IBaseIdentity<TId> where TId : class
    {
        string? UserName { get; set; }
        string? NormalizedUserName { get; set; }
        string? Email { get; set; }
        bool EmailConfirmed { get; set; }
        string NormalizedEmail { get; set; }
        string PasswordHash { get; }
        string SecurityStamp { get; set; }
        int AccessFailedCount { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
    }


    public class AvestaUser<TId> : BaseIdentity<TId>, IAvestaUser<TId>
        where TId : class
    {
        public AvestaUser() : base()
        {
        }
        public virtual string? UserName { get; set; }
        public virtual string? NormalizedUserName { get; set; }
        public virtual string? Email { get; set; }
        public virtual bool EmailConfirmed { get; set; }
        public virtual string NormalizedEmail { get; set; }
        public virtual string PasswordHash { get; private set; }
        public virtual string SecurityStamp { get; set; }
        public virtual int AccessFailedCount { get; set; }
        public virtual DateTimeOffset? LockoutEnd { get; set; }


        public ICollection<AvestaUserActivity<TId, IAvestaUser<TId>>> AvestaUserActivities { get; set; }
        public ICollection<AvestaUserToken<TId, IAvestaUser<TId>>> Tokens { get; set; }
        public ICollection<AvestaUserClaim<TId, IAvestaUser<TId>>> Claims { get; set; }


        public override string ToString()
        {
            var result = $"{UserName}-{Email}";
            return result;
        }

        public override bool Equals(object? obj)
        {
            if (obj is AvestaUser<TId>)
            {
                var u1 = obj as AvestaUser<TId>;
                _ = u1 ?? throw new ArgumentNullException(nameof(obj));
                return (u1.UserName == this.UserName && u1.NormalizedUserName == this.NormalizedUserName && u1.PasswordHash == this.PasswordHash && u1.Email == this.Email)
                    || u1.Id == this.Id;
            }
            return false;
        }

        public virtual void SetPassword(string newPassword)
        {
            this.PasswordHash = newPassword.MakeItSha256();
        }
        public virtual bool ValidatePassword(string password)
        {
            return this.PasswordHash == password.MakeItSha256();
        }

    }

    public class AvestaUser<TId, TAvestaUserGroup> : AvestaUser<TId>
      where TId : class
      where TAvestaUserGroup : AvestaUserAuthorizeGroup<TId, AvestaUser<TId, TAvestaUserGroup>>
    {
        public virtual ICollection<TAvestaUserGroup>? UserAuthorizeGroups { get; set; }

    }



}
