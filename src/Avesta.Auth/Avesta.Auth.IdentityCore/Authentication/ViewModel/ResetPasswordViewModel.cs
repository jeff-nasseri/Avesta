using Avesta.Share.Model;
using Avesta.Constant;
using System.ComponentModel.DataAnnotations;

namespace Avesta.Auth.IdentityCore.Authentication.ViewModel
{
    public class ResetPasswordViewModel : BaseModel
    {

        public virtual string Password { get; set; }
        [Compare(nameof(Password))]
        public virtual string RepeatPassword { get; set; }
        public virtual string? UserPhonenumber { get; set; }
        public virtual string? Email { get; set; }
        public virtual string ResetPasswordToken { get; set; }
    }
}
