using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions.Invoice
{
    public class InvoiceException : SystemException
    {
        public InvoiceException(int code) : base(code)
        {
        }
    }
    public class CurrentInvoiceAlreadyHavePaymentResultException : InvoiceException
    {
        public CurrentInvoiceAlreadyHavePaymentResultException() : base(ExceptionConstant.CurrentInvoiceAlreadyHavePaymentResultException)
        {
        }
    }
}
