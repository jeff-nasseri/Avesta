using Avesta.Seed.Entity.Service;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed.Extension
{
    public static class SeedExtension
    {
        public static T CreateInstance<T>(ISeedDataGenerator generator)
        {
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            var obj = Activator.CreateInstance<T>();

            foreach (var prop in props)
            {
                var value = prop.GenerateValue(generator);
                prop.SetValue(obj, value);
            }

            return obj;
        }

        public static object GenerateValue(this PropertyInfo info, ISeedDataGenerator generator)
        {
            var definition = MemberDefinition.GetDefinition(info.Name);
            var value = generator.GenerateValueByDefinition<object>(definition);
            return value;
        }


    }
}
