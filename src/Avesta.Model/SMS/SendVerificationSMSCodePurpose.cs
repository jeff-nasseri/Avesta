using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Model.SMS
{
    public enum SendVerificationSMSCodePurpose
    {
        Non,
        UserRegister,
        DoctorRegister,
        ResetPassword,
        Login
    }
}
