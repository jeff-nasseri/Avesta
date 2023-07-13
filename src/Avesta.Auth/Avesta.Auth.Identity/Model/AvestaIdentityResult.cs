using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Identity.Model
{
    public class AvestaIdentityResult : ReturnTemplate<object>
    {
        public int IdentityStatus { get; set; }
        public string Message { get; set; }

        public static AvestaIdentityResult Ok(string message)
        {
            var newObj = new AvestaIdentityResult();
            newObj.IdentityStatus = 0;
            newObj.Succeed = true;
            newObj.Message = message;
            return newObj;
        }
        public static AvestaIdentityResult NotOk(string message)
        {
            var newObj = new AvestaIdentityResult();
            newObj.IdentityStatus = 500;
            newObj.Succeed = false;
            newObj.Message = message;
            return newObj;
        }
    }

}
