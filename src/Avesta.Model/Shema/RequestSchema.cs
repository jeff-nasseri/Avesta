using Avesta.Share.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Shema
{
    public abstract class RequestSchema
    {
        public string RequestId { get; protected set; }
        public RequestSchema()
        {
            RequestId = Keys.GenerateUniqueId();
        }
    }

}
