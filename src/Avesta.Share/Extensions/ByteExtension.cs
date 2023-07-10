using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static Avesta.Constant.UserFileOperationMessages.Field;

namespace Avesta.Share.Extensions
{
    public static class ByteExtension
    {

        public static byte[] GetRandomByte(int size = 256)
        {
            Random rnd = new Random();
            Byte[] b = new Byte[size];
            rnd.NextBytes(b);

            return b;
        }

        public static IEnumerable<byte[]> Split(this byte[] value, int number = 2)
        {
            if (value == null || value.Length == 0)
                throw new ArgumentNullException();

            if (value.Length < number)
                throw new Exception($"can not split value with lenght of {value.Length} to more then {number}");


            var result = new List<byte[]>();
            var c = value.Length / number;
            for (int i = 0; i < number; i++)
            {
                var last = ((i + 1) * c);
                var first = (i * c);
                var x = value[first..last];

                if(i == number - 1 && value.Length % number != 0)
                {
                    var data = value[(last)..(value.Length)].ToList();
                    var x2 = x.ToList();
                    x2.AddRange(data);
                    result.Add(x2.ToArray());
                    continue;
                }

                result.Add(x);

            }


            return result;
        }

    }
}
