using Avesta.Exceptions.Entity;
using Avesta.Storage.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions.Reflection
{
    public class ReflectionException : SystemException
    {
        public ReflectionException(int code) : base(code)
        {
        }

        public ReflectionException(string msg, int code) : base(msg, code)
        {
        }
    }

    public class CanNotFoundPropertyWithCurrentNameInCurrentType : ReflectionException
    {
        public CanNotFoundPropertyWithCurrentNameInCurrentType(string msg, int code = ExceptionConstant.CanNotFoundPropertyWithCurrentNameInCurrentType) : base(msg, code)
        {
        }
    }


}
