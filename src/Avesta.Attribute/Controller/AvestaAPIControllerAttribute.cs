using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Attribute.Controller
{

    [AttributeUsage(AttributeTargets.Class)]
    public class AvestaAPIControllerAttribute : ApiControllerAttribute
    {
    }

}
