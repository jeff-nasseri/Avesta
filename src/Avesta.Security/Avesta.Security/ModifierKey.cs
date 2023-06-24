using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Share.Extensions;

namespace Avesta.Security
{
    public sealed class ModifierKey
    {
        public ModifierKey()
        {
            ValueInByte = ByteExtension.GetRandomByte();
        }
        public ModifierKey(byte[] valueInByte)
        {
            ValueInByte = valueInByte;
        }

        public byte[] ValueInByte { get; private set; }

        public string ValueToBase64String() => Convert.ToBase64String(ValueInByte);

        public IEnumerable<ModifierKey> Split(int number = 2)
        {
            var result = ValueInByte.Split(number);
            foreach (var item in result)
            {
                yield return new ModifierKey
                {
                    ValueInByte = item,
                };
            }
        }
    }
}
