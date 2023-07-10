using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Utilities
{
    public class List
    {
        public static IEnumerable<TModel> Merge<TModel>(IEnumerable<TModel> list1, IEnumerable<TModel> list2) where TModel : class
        {
            List<TModel> result = new List<TModel>();

            result.AddRange(list1 ?? Enumerable.Empty<TModel>());
            result.AddRange(list2 ?? Enumerable.Empty<TModel>());

            return result;

        }


    }
    public static class StringUtls
    {
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }

    public static class ArrayUtls
    {
        public static string ArrayToString(ReadOnlyCollection<byte> arr)
        {
            StringBuilder s = new StringBuilder(arr.Count * 2);
            for (int i = 0; i < arr.Count; ++i)
            {
                s.AppendFormat("{0:x2}", arr[i]);
            }

            return s.ToString();
        }
    }

    public class Compare
    {
        public static IEnumerable<string> GetChangedPropertyNames<T, G>(T original, G changedObj, string[] excludes = null) where T : class
    where G : class
        {
            var changes = new List<string>();
            var propperties = typeof(T).GetProperties().ToList();
            foreach (var property in propperties)
            {
                if (excludes.Any(item => item.ToLower().Contains(property.Name.ToLower())))
                    continue;

                var originalValue = property.GetValue(original)?.ToString();
                var changedValue = property.GetValue(changedObj)?.ToString();
                if (originalValue != changedValue)
                {
                    changes.Add(property.Name);
                }
            }

            return changes;

        }

        public static IEnumerable<string> GetChangedPropertyNames<T>(T original, T changedObj, string[] excludes = null) 
            where T : class => GetChangedPropertyNames<T, T>(original, changedObj, excludes);

    }
}
