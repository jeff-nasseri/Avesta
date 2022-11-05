using Avesta.Share.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.CLI
{
    public abstract class CommandAttribute : System.Attribute
    {
        public string Id { get; }
        public string FullName { get; set; }
        public string Help { get; set; }
        public string Verbosity { get; set; }

        public CommandAttribute()
        {
            Id = Keys.GenerateUniqueId();
        }
        public CommandAttribute(string fullName, string help, string verbosity = "") : this()
        {
            FullName = fullName;
            Help = help;
            Verbosity = verbosity;
        }
    }
}