using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Interface
{

    public interface IMatchable
    {
        byte[] Matching(params ISecurityEntity[] keys);
        bool TryMatch(params ISecurityEntity[] keys);
    }

}
