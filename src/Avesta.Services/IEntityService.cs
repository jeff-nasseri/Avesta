using Avesta.Data.Model;
using Avesta.Services.Create;
using Avesta.Services.Delete;
using Avesta.Services.Graph;
using Avesta.Services.Read;
using Avesta.Services.Update;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services
{

    public interface IEntityService<TId, TEntity, TModel> : IReadEntityService<TId, TEntity, TModel>
        , IDeleteEntityService<TId, TEntity, TModel>
        , IUpdateEntityService<TId, TEntity, TModel>
        , ICreateEntityService<TId, TEntity, TModel>
        , IEntityGraphService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {
    }


    public interface IEntityService<TId, TEntity, TModel, TCreateModel, TEditModel> : IEntityService<TId, TEntity, TModel>
        , IUpdateEntityService<TId, TEntity, TModel, TEditModel>
        , ICreateEntityService<TId, TEntity, TModel, TCreateModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
        where TEditModel : TModel
    { }
}
