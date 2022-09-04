using Avesta.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.SMS
{
    public class PhoneNumberVerification : ExpiredModel
    {
        Random Random = new Random();
        public PhoneNumberVerification(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ID = PhoneNumber;
            Code = Random.Next(10001, 99999).ToString();
        }

        public string PhoneNumber { get; set; }
        public string Code { get; set; }

    }
}
