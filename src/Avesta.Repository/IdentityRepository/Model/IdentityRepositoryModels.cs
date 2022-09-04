using Microsoft.AspNetCore.Identity;
using Avesta.Model;
using Avesta.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Repository.Identity.Model
{

    public class IdentityRegisterUserReturn : IdentityRepositoryReturn
    {
        public IdentityResult AddToRoleResult { get; set; }
    }

    public class IdentityRepositoryReturn : ReturnTemplate
    {
    }
}
