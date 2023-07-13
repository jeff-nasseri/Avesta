using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Identity.Model
{
    public class Claim
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Provider { get; set; }
    }

}
