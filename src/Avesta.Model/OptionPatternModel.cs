using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model
{
    public class ReserveLicenseOption
    {
        public const string Key = nameof(ReserveLicenseOption);
        public int PaymentWaitTimeMin { get; set; }
        public int InvoicePageWaitTimeMin { get; set; }
    }

    public class BanIpSetting
    {
        public const string Key = nameof(BanIpSetting);
        public int TotalRequest { get; set; }
        public int MinTime { get; set; }
        public int ExitFromBanListMin { get; set; }
    }
}
