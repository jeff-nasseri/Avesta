using Avesta.Constant;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.MVC.Crud.Read
{

    public interface IReadController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        Task<IActionResult> Get(TId id);
        Task<IActionResult> GetAll(int? page = null, int perPage = Pagination.PerPage, string[] keyword = null);
    }

}
