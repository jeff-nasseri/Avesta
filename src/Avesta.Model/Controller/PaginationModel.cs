using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.Controller
{

    public class PaginationModel<T> : Model where T : class
    {
        public IEnumerable<T> Entities { get; set; }
        public int Total { get; set; }
    }
}
