using Avesta.Share.Model;
using Avesta.Storage.Constant;

namespace Avesta.Auth.Authentication.ViewModel
{
    public class ResetPasswordViewModel : BaseModel
    {
        public virtual string Password { get; set; }
        public virtual string RepeatPassword { get; set; }
        public virtual string UserPhonenumber { get; set; }
        public virtual string ResetPasswordToken { get; set; }
    }
}
