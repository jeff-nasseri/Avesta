using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model
{
    public class ReturnTemplate<TResult> : ReturnTemplate where TResult : class
    {
        public TResult Result { get; set; }
    }

    public class ReturnTemplate
    {
        public bool Succeed { get; set; }
        public string[] Errors { get; set; }
    }
}
