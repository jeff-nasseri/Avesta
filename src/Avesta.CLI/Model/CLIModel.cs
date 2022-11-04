using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI.Model
{

    public class CLIModel : Share.Model.Model
    {
        public string Help { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
    }
}
