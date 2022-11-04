using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Avesta.CLI.Model
{


    public class CLIMethodModel : CLIModel
    {
        public IEnumerable<CLIArgumentModel> Arguments { get; set; }
    }
}
