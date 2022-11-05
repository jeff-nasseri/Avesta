using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.CLI
{
    public class PropertyValues : Dictionary<string, object>
    {
        public PropertyValues()
        {

        }
        public PropertyValues(string key, object value)
        {
            base.Add(key, value);
        }
    }
}
