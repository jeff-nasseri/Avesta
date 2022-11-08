using Avesta.CLI.Context;
using Avesta.CLI.Model;
using Avesta.Share.Model.CLI;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
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
        readonly AvestaCommandContext _commandContext;
        public CommandExecuter(IServiceProvider serviceProvider, AvestaCommandContext commandContext)
        {
            _commandContext = commandContext;
            _serviceProvider = serviceProvider;
        }




        public virtual void Execute(InputCommandStruct command)
        {
            var caller = GetCaller(command);

            var obj = _serviceProvider.GetRequiredService(caller.ClassType);
            foreach (var prop in caller.Properties)
            {
                var name = prop.Key;
                var propertyInfo = obj.GetType().GetProperty(name);
                var value = Convert.ChangeType(prop.Value, propertyInfo.PropertyType);
                propertyInfo?.SetValue(obj, value, null);
            }

            var methodInfo = obj.GetType().GetMethod(caller.MethodName);
            var args = caller.Arguments.Select(a => Convert.ChangeType(a.value, a.type)).ToArray();

            methodInfo?.Invoke(obj, args);

        }


        public virtual MethodCaller GetCaller(InputCommandStruct command)
        {
            var model = _commandContext.SingleOrDefault(c => c.FullName.ToLower() == command.ClassName.ToLower());
            var method = model?.Methods.SingleOrDefault(m => m.RealName.ToLower() == command.MethodName.ToLower());


            //create customize exception for this one
            if (model == null)
                throw new Exception("command not found !");

            var propertyValues = new PropertyValues();

            foreach (var prop in command.PropertyValues)
            {
                if (prop.Key.StartsWith("--"))
                {
                    var fullName = prop.Key.Substring(2);
                    var targetProp = model.Properties.SingleOrDefault(p => p.FullName.ToLower() == fullName.ToLower());
                    if (targetProp == null)
                        continue;

                    propertyValues.Add(targetProp?.RealName, prop.Value);
                }
                if (prop.Key.StartsWith('-'))
                {
                    var shortName = prop.Key.Substring(1);
                    var targetProp = model.Properties.SingleOrDefault(p => p.ShortName.ToLower() == shortName.ToLower());
                    if (targetProp == null)
                        continue;

                    propertyValues.Add(targetProp?.RealName, prop.Value);
                }

            }


            var argumentValues = new ArgumentValues();

            foreach (var arg in command.ArgumentValues)
            {

                if (arg.name.StartsWith("--"))
                {
                    var fullName = arg.name.Substring(2);
                    var targetArg = method?.Arguments.SingleOrDefault(a => a.FullName.ToLower() == fullName.ToLower());
                    if (targetArg == null)
                        continue;

                    argumentValues.Add(new(targetArg.Type, targetArg?.RealName, arg.value));
                }
                if (arg.name.StartsWith('-'))
                {
                    var shortName = arg.name.Substring(1);
                    var targetArg = method?.Arguments.SingleOrDefault(a => a.ShortName.ToLower() == shortName.ToLower());
                    if (targetArg == null)
                        continue;

                    argumentValues.Add(new(targetArg.Type, targetArg?.RealName, arg.value));
                }

            }





            return new MethodCaller
            {
                ClassName = model.RealName,
                MethodName = method.RealName,
                Arguments = argumentValues,
                Properties = propertyValues,
                ClassType = model.Type
            };
        }




    }

}
