using Avesta.Share.Model.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class CLI
    {
        public static InputCommandStruct CommandParser(string commandStr)
        {
            var argIndexes = new List<int>();

            var command = Regex.Replace(commandStr, @"\s+", " ");
            var phases = command.Split(" ").Where(item => !string.IsNullOrEmpty(item)).ToList();

            var className = phases.First();
            phases.Remove(className);

            var argNames = phases.Where(p => p.StartsWith('-') || p.StartsWith("--")).ToList();
            foreach (var name in argNames)
            {
                var index = phases.IndexOf(name);
                var value = phases[index + 1];
                argIndexes.Add(index);
                argIndexes.Add(index + 1);
            }
            var methodName = phases.FirstOrDefault(p => !argIndexes.Any(i => i == phases.IndexOf(p)));

            var parts = command.Split(methodName).ToList();

            var properties = parts.FirstOrDefault();
            var arguments = parts.LastOrDefault();


            var propertyPhase = properties.Split(" ").ToList();
            var propertyArgs = propertyPhase.Where(p => p.StartsWith('-') || p.StartsWith("--")).ToList();
            var propertieValues = new PropertyValues();
            foreach (var name in propertyArgs)
            {
                var index = propertyPhase.IndexOf(name);
                var value = propertyPhase[index + 1];
                propertieValues.Add(name, value);
            }



            var argumentPhase = arguments.Split(" ").ToList();
            var argumentArgs = argumentPhase.Where(p => p.StartsWith('-') || p.StartsWith("--")).ToList();
            var argumentValues = new ArgumentValues();
            foreach (var name in argumentArgs)
            {
                var index = argumentPhase.IndexOf(name);
                var value = argumentPhase[index + 1];
                argumentValues.Add(name, value);
            }


            var result = new InputCommandStruct
            {
                ClassName = className,
                ArgumentValues = argumentValues,
                PropertyValues = propertieValues,
                MethodName = methodName
            };

            return result;

        }

    }
}
