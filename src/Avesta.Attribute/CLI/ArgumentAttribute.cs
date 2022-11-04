using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.CLI
{


    [AttributeUsage(AttributeTargets.Method)]
    public class ArgumentAttribute : CommandAttribute
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public Type ArgumentType { get; set; }
        public ArgumentAttribute(string shortName, string fullName, string help, Type argumentType) : base(name: fullName, help: help)
        {
            ShortName = shortName;
            FullName = fullName;
            ArgumentType = argumentType;
        }
    }
}
