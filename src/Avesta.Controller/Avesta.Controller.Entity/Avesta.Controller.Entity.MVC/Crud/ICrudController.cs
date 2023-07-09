using Avesta.Controller.Entity.MVC.Crud.Delete;
using Avesta.Controller.Entity.MVC.Crud.Read;
using Avesta.Controller.Entity.MVC.Crud.Update;
using Avesta.Controller.Entity.MVC.Crud.Create;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;

namespace Avesta.Controller.Entity.MVC.Crud
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
