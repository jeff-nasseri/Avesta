using Avesta.Storage.Constant;
using Avesta.Exceptions.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions.Identity
{

    public class IdentityException : SystemException
    {
        public IdentityException(string msg, int code = ExceptionConstant.IdentityException) : base(msg, code)
        {
        }
    }
    public class SMSVerificationCodeIsWrong : IdentityException
    {
        public SMSVerificationCodeIsWrong(string msg) : base(msg, ExceptionConstant.SMSVerificationCodeIsWrong)
        {
        }
    }
    public class UserNotFoundException : IdentityException
    {
        public UserNotFoundException(string userIDOrMsg) : base(userIDOrMsg, code: ExceptionConstant.UserNotFoundException)
        {
        }
    }
    public class PasswordIsWorngException : IdentityException
    {
        public PasswordIsWorngException(string userID) : base(userID, code: ExceptionConstant.PasswordIsWorngException)
        {
        }
    }

    public class CanNotFoundAnyUserWithThisUsernameAndPassword : IdentityException
    {
        public CanNotFoundAnyUserWithThisUsernameAndPassword(string msg) : base(msg, ExceptionConstant.CanNotFoundAnyUserWithThisUsernameAndPassword)
        {
        }
    }
}
