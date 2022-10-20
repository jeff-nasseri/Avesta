using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Extensions
{
    public static class MapperExtension
    {
        public static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }


        public static T UpdateBy<T>(this T obj, T updated) where T : class
        {
            var properties = updated.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(updated);
                if (value == null)
                    continue;

                property.SetValue(obj, value);

            }


            return obj;
        }




        public static TSource UpdateBy<TSource,TUpdated>(this TSource source,TUpdated updated) 
            where TSource : class 
            where TUpdated : class
        {
            var properties = updated.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(updated);
                if (value == null)
                    continue;

                var prop = source.GetType().GetProperty(property.Name);
                if (prop == null)
                    continue;

                prop.SetValue(source, value);

            }

            return source;
        }




    }
}
