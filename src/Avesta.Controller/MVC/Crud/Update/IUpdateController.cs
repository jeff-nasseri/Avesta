using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.MVC.Crud.Update
{

    public interface IUpdateController<TId, TEntity, TModel>
    where TId : class
    where TEntity : BaseEntity<TId>
    where TModel : BaseModel<TId>
    {
        Task<IActionResult> Update(TModel model);
        Task<IActionResult> Update(TId id);
    }
    public interface IUpdateController<TId, TEntity, TModel, TEditModel> : IUpdateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
    {
        Task<IActionResult> Update(TEditModel model);
    }
}
