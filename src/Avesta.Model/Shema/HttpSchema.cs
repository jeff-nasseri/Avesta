using Avesta.Attribute.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Shema
{


    public abstract class HttpSchema : RequestSchema
    {
        [RequestHeader("accept")]
        public virtual string Accept { get; set; }

        [RequestHeader("origin")]
        public virtual string Origin { get; set; }

        [RequestHeader("method")]
        public virtual string Method { get; set; }

    }


}
