using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.CLI
{


    [AttributeUsage(AttributeTargets.Method)]
    public class MethodAttribute : CommandAttribute
    {
        public MethodAttribute(string fullName, string help) : base(fullName: fullName, help: help)
        {
        }
    }


}
