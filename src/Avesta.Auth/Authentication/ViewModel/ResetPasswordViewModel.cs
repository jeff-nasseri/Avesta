using Avesta.Share.Attribute;
using Avesta.Share.Model;
using Avesta.Storage.Constant;

namespace Avesta.Auth.Authentication.ViewModel
{
    public class ResetPasswordViewModel : BaseModel
    {
        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.Password)]
        //[LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.Password)]
        public string Password { get; set; }

        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.RepeatPassword)]
        //[LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.RepeatPassword)]
        public string RepeatPassword { get; set; }

        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.PhoneNumber)]
        //[LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.PhoneNumber)]
        public string UserPhonenumber { get; set; }

        public string ResetPasswordToken { get; set; }
    }
}
