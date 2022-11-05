using AutoMapper;
using Avesta.Attribute.CLI;
using Avesta.CLI.Context;
using Avesta.CLI.Model;
using Avesta.Share.Model.CLI;
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



        public virtual void OnProcess(IEnumerable<Type> types)
        {

            foreach (var type in types)
            {

                var classAttribute = type.GetCustomAttribute<ClassAttribute>();
                var classModel = _mapper.Map<CLIClassModel>(classAttribute);
                classModel.Type = type;
                classModel.RealName = type.Name;

                var properties = type.GetProperties().Where(prop => prop.GetCustomAttribute<PropertyAttribute>() != null);
                var CLIPropertyModels = properties.Select(prop => new CLIPropertyModel
                {
                    FullName = prop.GetCustomAttribute<PropertyAttribute>()?.FullName,
                    RealName = prop.Name,
                    Help = prop.GetCustomAttribute<PropertyAttribute>()?.Help,
                    ShortName = prop.GetCustomAttribute<PropertyAttribute>()?.ShortName,
                    Type = prop.GetType()
                });
                classModel.Properties = CLIPropertyModels;

                var methods = type.GetMethods().Where(m => m.GetCustomAttribute<MethodAttribute>() != null);
                var CLIMethodModels = methods.Select(method => new CLIMethodModel
                {
                    RealName = method.Name,
                    Help = method.GetCustomAttribute<MethodAttribute>()?.Help,
                    FullName = method.GetCustomAttribute<MethodAttribute>()?.FullName,
                    Type = method.GetType(),
                    Arguments = method.GetCustomAttributes<ArgumentAttribute>().Select(a => new CLIArgumentModel
                    {
                        FullName = a.FullName,
                        Help = a.Help,
                        ShortName = a.ShortName,
                        Type = a.ArgumentType,
                    }).ToList()
                });
                classModel.Methods = CLIMethodModels;

                _commandContext.Add(classModel);
            }
        }




    }






}
