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
    public class IdentityModel
    {
        public string ID { get; set; }

        [LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.Password)]
        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.Password)]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.Email)]
        [LocalizeDataType(DataType.EmailAddress, messageCode: AccountOperationMessages.Error.DataType.Email)]
        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.Email)]
        public string Email { get; set; }

        [LocalizedDisplayName(messageCode: AccountOperationMessages.Field.Name.Username)]
        [LocalizeRequired(messageCode: AccountOperationMessages.Error.Required.Username)]
        public string Username => Email;
    }
    public class LockOutCycle
    {
        public static DateTime Infinit = new DateTime(2999, 1, 1);
        public static DateTime OpenLock = new DateTime(1999, 1, 1);
    }
}
