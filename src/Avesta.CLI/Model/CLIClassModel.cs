using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Avesta.CLI.Model
{


    public class CLIClassModel : CLIModel
    {
        public IEnumerable<CLIPropertyModel> Properties { get; set; }
        public IEnumerable<CLIMethodModel> Methods { get; set; }
    }
}
