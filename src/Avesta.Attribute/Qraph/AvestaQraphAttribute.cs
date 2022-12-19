using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Qraph
{

    public class AvestaQraphAttribute : HttpMethodAttribute
    {
        public AvestaQraphAttribute() : base(new List<string> { "GET" })
        {
        }

        public AvestaQraphAttribute(string template) : base(new List<string> { "GET" }, template)
        {
        }
    }
}
