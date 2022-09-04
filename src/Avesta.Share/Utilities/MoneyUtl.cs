using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Utilities
{
    public static class MoneyUtl
    {
        public static int ToIRT(this int IRR)
        {
            return IRR * 10;
        }
        public static int ToIRR(this int IRT)
        {
            return IRT / 10;
        }
        public static int ToIRT(this string IRR)
        {
            return int.Parse(IRR).ToIRT();
        }
    }
   
}
