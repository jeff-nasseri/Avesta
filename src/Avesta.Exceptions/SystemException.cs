using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions
{
    public class SystemException : Exception
    {
        public int Code { get; set; }
        public SystemException(int code)
        {
            Code = code;
        }
        public SystemException(string msg, int code) : base(msg)
        {
            Code = code;
        }
    }









}
