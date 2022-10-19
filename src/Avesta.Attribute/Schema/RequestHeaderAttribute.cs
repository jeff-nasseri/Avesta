using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Schema
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequestHeaderAttribute : RequestSchemaAttribute
    {
        public RequestHeaderAttribute(string key) : base(key)
        {
        }
    }


}
