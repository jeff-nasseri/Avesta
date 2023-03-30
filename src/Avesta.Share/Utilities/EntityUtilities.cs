using Avesta.Share.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{

    public static class EntityUtilites
    {



        public static async Task<bool> Contains<T>(this T data, params object[] keywords)
        {
            await Task.CompletedTask;

            if (keywords == null || keywords.Count() == 0)
                return false;


            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

            foreach (var property in properties)
            {
                var value = property.GetValue(data);
                var serialize = JsonConvert.SerializeObject(value);

                if (keywords.Any(k => k.ToString().ToUpper().Contains(serialize.ToUpper())))
                    return true;
            }

            return false;
        }


        public static async Task<IEnumerable<T>> Search<T>(this IEnumerable<T> entityList, params object[] keywords)
        {

            if (keywords == null || keywords.Count() == 0)
                return entityList;

            var result = new List<T>();

            foreach (var entity in entityList)
            {
                if (await entity.Contains(keywords))
                    result.Add(entity);
            }

            return result;
        }











        /// <summary>
        /// search on string,number properties of the type (T) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList">the list you want to be searched</param>
        /// <param name="keyword">your search keywords, for multiple keyword you should split them by ','</param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> Search<T>(this IEnumerable<T> entityList, string keyword)
        {
            await Task.CompletedTask;

            if (string.IsNullOrEmpty(keyword))
                return entityList;


            keyword.GetSearchParam(out List<string> words);

            var resultList = new List<T>();
            foreach (var entity in entityList)
            {
                Func<PropertyInfo, bool> check = (p) =>
                {
                    if (p.PropertyType == typeof(string) || p.PropertyType.IsNumericData())
                    {
                        var data = p.GetValue(entity)?.ToString().ToLower();
                        if (string.IsNullOrEmpty(data))
                            return false;
                        var any = words.Any(w => data.Contains(w.ToLower()));
                        return any;
                    }
                    return false;
                };
                var result = entity.GetType().GetProperties().Any(check);
                if (result)
                {
                    resultList.Add(entity);
                }
            }
            return resultList;
        }



        /// <summary>
        /// This method run for all datas even data you dont need 
        /// After fetch data use this function if you want to paginate and search into your data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="searchKeyWord"></param>
        /// <returns></returns>
        public static async Task<(IEnumerable<T>, int)> Paginate<T>(this IEnumerable<T> entityList, int page, int perPage = 1, string searchKeyWord = null)
        {
            var products = default(IEnumerable<T>);
            var count = 0;
            if (!string.IsNullOrEmpty(searchKeyWord))
            {
                var total = (await Search(entityList, searchKeyWord));
                products = total.Skip(perPage * (page - 1)).Take(perPage).ToList();
                count = total.Count();
            }
            else
            {
                products = entityList.Skip(perPage * (page - 1)).Take(perPage).ToList();
                count = entityList.Count();
            }
            return (products, count);
        }
    }

}
