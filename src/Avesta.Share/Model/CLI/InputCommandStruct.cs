using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model.CLI
{
    public class InputCommandStruct : Model
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public ArgumentValues ArgumentValues { get; set; }
        public PropertyValues PropertyValues { get; set; }
    }
}
