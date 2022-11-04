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
        public ClassAttribute(string name, string help) : base(name: name, help: help)
        {
        }
    }



}
