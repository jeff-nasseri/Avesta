using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.API.Crud.Delete
{

    public interface IDeleteController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Delete(TId id);
        Task<IActionResult> SoftDelete(TId id);
    }

}
