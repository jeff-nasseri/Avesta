using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class PropertyValues : Dictionary<string, object>
    {
        public PropertyValues(string key, object value)
        {
            base.Add(key, value);
        }
    }
    public class ArgumentValues : Dictionary<string, object>
    {
        public ArgumentValues(string key, object value)
        {
            base.Add(key, value);
        }
    }
}
