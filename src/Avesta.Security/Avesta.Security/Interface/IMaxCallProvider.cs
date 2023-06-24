using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Interface
{

    public interface IMaxCallProvider
    {
        IEnumerable<(string method, int numberOfTry)> Values { get; }
        void IncreaseCall(string method);
    }

}
