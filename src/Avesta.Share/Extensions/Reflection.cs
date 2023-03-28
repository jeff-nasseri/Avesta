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



        /// <summary>
        /// Search all properties in list of TEntity type and make sure there is no value null in property type of PropEntity
        /// </summary>
        /// <typeparam name="TEntity">The type of object</typeparam>
        /// <typeparam name="PropEntity">The type of property</typeparam>
        /// <param name="list">List of TEntity objects</param>
        /// <returns>return true if there is not found any PropEntity property with value null, otherwise false</returns>
        public static bool CheckTheValueOfEntity<TEntity, PropEntity>(this IEnumerable<TEntity> list)
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




    }
}
