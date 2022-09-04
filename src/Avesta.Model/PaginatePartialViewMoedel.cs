using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public class PaginatePartialViewMoedel : Private.Share
    {
    }
    public class SearchPartialViewModel : Private.Share
    {
    }
    namespace Private
    {
        public class Share
        {
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
        }
    }
}
