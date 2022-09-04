using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class List
    {
        public static IEnumerable<TModel> Merge<TModel>(IEnumerable<TModel> list1, IEnumerable<TModel> list2) where TModel : class
        {
            List<TModel> result = new List<TModel>();

            result.AddRange(list1 ?? Enumerable.Empty<TModel>());
            result.AddRange(list2 ?? Enumerable.Empty<TModel>());

            return result;

        }
    }
}
