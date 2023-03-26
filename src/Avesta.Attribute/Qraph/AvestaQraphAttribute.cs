using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Graph
{

    public class AvestaGraphAttribute : HttpMethodAttribute
    {
        public AvestaGraphAttribute() : base(new List<string> { "GET" })
        {
        }

        public AvestaGraphAttribute(string template) : base(new List<string> { "GET" }, template)
        {
        }
    }
}
