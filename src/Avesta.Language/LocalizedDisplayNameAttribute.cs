using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Attribute
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        public LocalizedDisplayNameAttribute(int messageCode)
            : base(GetMessageFromResource(messageCode))
        { }

        private static string GetMessageFromResource(int messageCode)
        {
            return string.Empty;
        }
    }



}
