using Avesta.Share.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Avesta.Security.Hash.Extension
{
    public static class Sha256Extensions
    {
        public static string MakeItSha256(this object data)
        {
            var output = (byte[])(TypeDescriptor.GetConverter(data).ConvertTo(data, typeof(byte[])) ?? throw new InvalidOperationException("can not convert data to byte array!"));
            
            var base64 = Convert.ToBase64String(output);
            ReadOnlyCollection<byte> hash = Sha256.HashStream(StringUtls.GenerateStreamFromString(base64));

            var result = ArrayUtls.ArrayToString(hash);
            return result;
        }
    }
}
