using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.CLI
{


    [AttributeUsage(AttributeTargets.Class)]
    public class ClassAttribute : CommandAttribute
    {
        public ClassAttribute(string fullName, string help) : base(fullName: fullName, help: help)
        {
        }
    }



}
