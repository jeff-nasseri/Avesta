using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class PaginationUtils
    {
        public static void Paginate(out int skip, int perPage, int? page = null)
        {
            if (page == null)
            {
                skip = perPage;
                return;
            }

            skip = (page.Value - 1) * perPage;
        }
    }
}
