using Avesta.Attribute;
using Avesta.Storage.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Identity
{
    public class RegisterModelBase : IdentityModel
    {
        [LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.RepeatPassword)]
        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.RepeatPassword)]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public virtual string RepeatPassword { get; set; }




        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.PhoneNumber)]
        //[LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.PhoneNumber)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

    }

}
