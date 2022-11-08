using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.CLI
{
    public class ArgumentValues : List<(Type type, string name, object value)>
    {
        public ArgumentValues(Type type, string name, object value)
        {
            Add(new (type, name, value));
        }
        public ArgumentValues()
        {
        }
    }
}
