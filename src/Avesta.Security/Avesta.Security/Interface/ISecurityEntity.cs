using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Security.Interface
{

    public interface ISecurityEntity : IEntityModifier, IMatchable, IEquatable<ISecurityEntity>
    {
        public string Name { get; set; }
    }

}
