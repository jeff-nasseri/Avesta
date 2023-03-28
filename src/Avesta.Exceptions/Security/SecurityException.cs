using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions.Security
{   
    public class SecurityException : SystemException
    {
        public SecurityException(int code) : base(code) 
        {
        }
    }

    public class TooManyRequestException : SecurityException
    {
        public TooManyRequestException() : base(Constant.ExceptionConstant.TooManyRequestException)
        {
        }
    }
}
