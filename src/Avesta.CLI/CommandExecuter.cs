using Avesta.CLI.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI
{
    public class CommandExecuter
    {

        readonly IServiceProvider _serviceProvider;
        public CommandExecuter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }




        public void Execute(MethodCaller caller)
        {

            var obj = _serviceProvider.GetRequiredService(caller.ClassType);
            foreach (var prop in caller.Properties)
            {
                var name = prop.Key;
                var propertyInfo = obj.GetType().GetProperty(name);
                propertyInfo?.SetValue(obj, prop.Value, null);
            }

            var methodInfo = obj.GetType().GetMethod(caller.MethodName);
            var args = caller.Arguments.Select(a => a.Value).ToArray();
            methodInfo?.Invoke(obj, args);

        }
    }

}
