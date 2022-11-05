using Avesta.Share.Model.CLI;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyValues = Avesta.Share.Model.CLI.PropertyValues;

namespace Avesta.CLI.Model
{
    public class MethodCaller
    {
        public string ClassName { get; set; }
        public Type ClassType { get; set; }
        public string MethodName { get; set; }
        public PropertyValues Properties { get; set; }
        public ArgumentValues Arguments { get; set; }
    }


}
