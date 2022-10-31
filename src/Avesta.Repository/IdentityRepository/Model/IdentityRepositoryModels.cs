using Avesta.Share.Model;
using Microsoft.AspNetCore.Identity;

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
