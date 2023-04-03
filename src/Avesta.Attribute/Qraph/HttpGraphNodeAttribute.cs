using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Qraph
{



    public class HttpGraphNodeAttribute : HttpGetAttribute
    {
        public HttpGraphNodeAttribute() : base()
        {
        }

        public HttpGraphNodeAttribute(string template) : base(template)
        {
        }
    }


}
