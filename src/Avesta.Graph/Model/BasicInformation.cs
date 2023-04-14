using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Model
{

    public class BasicInformation : AvestaGraphModel
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? TypeFullName { get; set; }
        public string? TypeShowName { get; set; }
    }

}
