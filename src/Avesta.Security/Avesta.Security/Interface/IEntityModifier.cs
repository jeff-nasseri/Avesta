using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Interface
{

    public interface IEntityModifier
    {
        IModifier Modifier { get; }
        ISecurityEntity SetModifier(IModifier modifier);
    }

}
