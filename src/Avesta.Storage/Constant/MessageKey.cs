using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Storage.Constant
{
    //from 100000 to 150000
    public class MessageKey
    {
        public const string Prefix = "message.key.code.";

        public class FeaturesNeedTemplate
        {
            public const int SendSMSAfterBuy = 100000;
            public const int SendEmailAfterBuy = 100001;
        }
    }
}
