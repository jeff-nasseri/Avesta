using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.MVC.Crud.Create
{



    public interface ICreateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
        Task<IActionResult> Create(TModel model);
        Task<IActionResult> Create();
    }
    public interface ICreateController<TId, TEntity, TModel, TCreateModel> : ICreateController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
    {
        Task<IActionResult> Create(TCreateModel model);
    }

}
