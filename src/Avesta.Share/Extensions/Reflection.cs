using Microsoft.AspNetCore.Mvc;
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

        public static bool IsNumericData(this Type type)
        {
            var result = type == typeof(int) || type == typeof(float) || type == typeof(decimal) || type == typeof(double);
            return result;
        }


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




        public static bool ValidateIncludeAllChildren<TEntity, PropEntity>(this IEnumerable<TEntity> list)
            where TEntity : class
            where PropEntity : class
        {
            var props = typeof(TEntity).GetProperties().ToList();
            var baseEntityProperties = props.Where(p => p.PropertyType.IsSubclassOf(typeof(PropEntity)) || p.PropertyType.BaseType == typeof(PropEntity)).ToList();

            foreach (var item in list)
            {
                foreach (var prop in baseEntityProperties)
                {
                    var valueOf = typeof(TEntity).GetProperty(prop.Name)?.GetValue(item);
                    if (valueOf == null)
                        return false;
                }
            }
            return true;
        }


        public static IEnumerable<TEntity> SetAllToNull<TEntity, PropEntity>(this IEnumerable<TEntity> list)
        {
            var props = typeof(TEntity).GetProperties().ToList();
            var baseEntityProperties = props.Where(p => p.PropertyType.IsSubclassOf(typeof(PropEntity)) || p.PropertyType.BaseType == typeof(PropEntity)).ToList();

            foreach (var item in list)
            {
                foreach (var prop in baseEntityProperties)
                {
                    typeof(TEntity).GetProperty(prop.Name)?.SetValue(item, null);
                }
            }

            return list;
        }

        public static IEnumerable<Type> GetAllDrivenTypes<TEntity>(this Assembly assembly)
        {

            var types = assembly.GetTypes().Where(t => !t.IsAbstract
                            && t.IsSubclassOf(typeof(TEntity))
                            && t.BaseType == typeof(TEntity)).ToList();

            return types;
        }



    }
}
