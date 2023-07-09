using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Entity.Availability
{
    public interface IAvailabilityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<bool> Any(TModel model, string navigationPropertyPath = null);
        Task<bool> Any(TId id, string navigationPropertyPath = null);
        Task<bool> Any(Expression<Func<TEntity, bool>> expression, string navigationPropertyPath = null);



        Task CheckAvailability(TModel model, string navigationPropertyPath = null);
        Task CheckAvailability(TId id, string navigationPropertyPath = null);
        Task CheckAvailability(Expression<Func<TEntity, bool>> expression, string navigationPropertyPath = null);


    }




  






}
