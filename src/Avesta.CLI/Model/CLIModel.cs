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
        public virtual string Help { get; set; }
        public virtual string RealName { get; set; }
        public virtual string FullName { get; set; }
        public virtual Type Type { get; set; }
    }
}
