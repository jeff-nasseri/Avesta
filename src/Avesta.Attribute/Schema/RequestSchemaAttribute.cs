using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Schema
{
    public abstract class RequestSchemaAttribute : System.Attribute
    {
        public string Key { get; set; }
        public RequestSchemaAttribute(string key)
        {
            Key = key;
        }

    }
}
