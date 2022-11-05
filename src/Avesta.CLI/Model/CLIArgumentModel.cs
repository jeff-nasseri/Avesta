using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI.Model
{
    public class CLIArgumentModel : CLIModel
    {
        public string ShortName { get; set; }

        public override string RealName { get => base.FullName; }

    }
}
