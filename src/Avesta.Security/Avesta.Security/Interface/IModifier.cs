using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Interface
{
    public interface IModifier : IEquatable<IModifier>
    {
        string Name { get; set; }
        byte[] Body { get; }
        ModifierKey Key { get; }
        int HorizontalLevel { get; }
        int VerticalLevel { get; }
        int ZLevel { get; }

        string GetStringFromBody();
        byte[] GetBodyFromString(string str);

        IEnumerable<IModifier> MakePairs();
        IEnumerable<IModifier> Split(int number = 2);

        byte[] Match(params IModifier[] modifiers);
        bool IsMatch(params IModifier[] modifiers);
    }
}
