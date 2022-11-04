using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.CLI
{

    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : CommandAttribute
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public PropertyAttribute(string shortName, string fullName, string help) : base(name: fullName, help: help)
        {
            ShortName = shortName;
            FullName = fullName;
        }
    }
}
