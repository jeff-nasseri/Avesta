using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Extensions
{
    public static class ReflectionExtension
    {
        public static bool EnSureHasProperty(this Type type, string name)
        {
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name.ToLower();
                if (propName == name.ToLower())
                    return true;
            }
            return false;
        }   
        
        public static PropertyInfo? EnsureGetProperty(this Type type, string name)
        {
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name.ToLower();
                if (propName == name.ToLower())
                    return prop;
            }
            return null;
        }
    }
}
