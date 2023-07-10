using Avesta.Data.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Data.Identity.Model
{
    public class BaseIdentity<TId> : BaseEntity<TId>, IBaseIdentity<TId>
        where TId : class
    {
    }
    public class BaseIdentity : BaseIdentity<string> { }

    public interface IBaseIdentity<TId> : IBaseEntity<TId>
    where TId : class
    { }

}
