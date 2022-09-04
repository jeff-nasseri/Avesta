using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public class ErrorModel : BaseModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
    }
}
