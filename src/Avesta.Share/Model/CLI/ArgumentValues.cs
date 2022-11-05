using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.CLI
{
    public class ArgumentValues : Dictionary<string, object>
    {
        public ArgumentValues()
        {

        }
        public ArgumentValues(string key, object value)
        {
            base.Add(key, value);
        }
    }
}
