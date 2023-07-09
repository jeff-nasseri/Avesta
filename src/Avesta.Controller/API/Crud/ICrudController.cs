using Avesta.Controller.API.Crud.Create;
using Avesta.Controller.API.Crud.Delete;
using Avesta.Controller.API.Crud.Read;
using Avesta.Controller.API.Crud.Update;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Controller.API.Crud
{

    public interface ICrudController<TId, TEntity, TModel> : IReadController<TId, TEntity, TModel>
        , ICreateController<TId, TEntity, TModel>
        , IUpdateController<TId, TEntity, TModel>
        , IDeleteController<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
    }


    public interface ICrudController<TId, TEntity, TModel, TCreateModel, TEditModel> : ICrudController<TId, TEntity, TModel>
        , IUpdateController<TId, TEntity, TModel, TEditModel>
        , ICreateController<TId, TEntity, TModel, TCreateModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
        where TCreateModel : TModel
    {
    }

}
