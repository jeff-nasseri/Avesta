using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Signature
{

    public interface ISearchable<TResult>
    {
        Task<TResult> Search(params Expression[] expressions);
        Task<TResult> Search(params object[] keywords);
    }

}
