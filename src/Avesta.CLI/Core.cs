using AutoMapper;
using Avesta.Attribute.CLI;
using Avesta.CLI.Context;
using Avesta.CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.CLI
{

    public class Core
    {
        readonly AvestaCommandContext _commandContext;
        readonly IMapper _mapper;
        public Core(IMapper mapper, AvestaCommandContext commandContext)
        {
            _mapper = mapper;
            _commandContext = commandContext;
        }



        public void OnProcess(IEnumerable<Type> types)
        {

            foreach (var type in types)
            {

                var classAttribute = type.GetCustomAttribute<ClassAttribute>();
                var classModel = _mapper.Map<CLIClassModel>(classAttribute);
                classModel.Type = type;

                var properties = type.GetProperties().Where(prop => prop.GetCustomAttribute<PropertyAttribute>() != null);
                var CLIPropertyModels = properties.Select(prop => new CLIPropertyModel
                {
                    FullName = prop.GetCustomAttribute<PropertyAttribute>().FullName,
                    Name = prop.GetCustomAttribute<PropertyAttribute>().Name,
                    Help = prop.GetCustomAttribute<PropertyAttribute>().Help,
                    ShortName = prop.GetCustomAttribute<PropertyAttribute>().ShortName,
                    Type = prop.GetType()
                });
                classModel.Properties = CLIPropertyModels;

                var methods = type.GetMethods().Where(m => m.GetCustomAttribute<MethodAttribute>() != null);
                var CLIMethodModels = methods.Select(method => new CLIMethodModel
                {
                    Name = method.GetCustomAttribute<MethodAttribute>().Name,
                    Help = method.GetCustomAttribute<MethodAttribute>().Help,
                    Type = method.GetType(),
                    Arguments = method.GetCustomAttributes<ArgumentAttribute>().Select(a => new CLIArgumentModel
                    {
                        FullName = a.FullName,
                        Name = a.Name,
                        Help = a.Help,
                        ShortName = a.ShortName,
                        Type = a.ArgumentType
                    }).ToList()
                });
                classModel.Methods = CLIMethodModels;

                _commandContext.Add(classModel);
            }
        }
    }

}
