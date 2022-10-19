using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequestCookieAttribute : RequestSchemaAttribute
    {
        public RequestCookieAttribute(string key) : base(key)
        {
        }
    }


}
