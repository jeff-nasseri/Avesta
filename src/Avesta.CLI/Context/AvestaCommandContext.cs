using Avesta.CLI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI.Context
{

    public class AvestaCommandContext : List<CLIClassModel>
    {
        public virtual int TotalCommandNumber { get => this.Count; }
    }

}
