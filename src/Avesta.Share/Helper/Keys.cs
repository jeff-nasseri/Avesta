using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Helper
{
    public static class Keys
    {
        public static string GenerateUniqueKey()
        {
            var result = Guid.NewGuid().ToString();
            return result;
        }

        public static string GenerateUniqueId()
        {
            var result = Guid.NewGuid().ToString();
            return result;
        }

    }
}
