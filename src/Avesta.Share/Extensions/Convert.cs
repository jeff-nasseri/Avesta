using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Extensions
{
    public static class ConvertExtension
    {
        public static TChild Convert<TChild>(this object parent)
            where TChild : class
        {
            var json = JsonConvert.SerializeObject(parent);
            var child = JsonConvert.DeserializeObject<TChild>(json);
            return child;
        }
    }
}
