using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Model
{
    public class AvestaUser : IdentityUser
    {
        public AvestaUser()
        {
            RegisterDate = DateTime.Now;
            ModifiedDate = RegisterDate;
        }

        public DateTime RegisterDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FullName { get => $"{FirstName} {LastName}"; }




        #region [-JWT-]
        public virtual string RefreshToken { get; set; }

        #endregion

    }



}
