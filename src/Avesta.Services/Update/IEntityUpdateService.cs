using AutoMapper;
using Avesta.Data.Entity.Model;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Update
{
    public interface IUpdateEntityService<TId, TEntity, TModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
    {

        Task Update(TModel model, bool exceptionRaiseIfNotExist = false);

    }


    public interface IUpdateEntityService<TId, TEntity, TModel, TEditModel> : IUpdateEntityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
    {
        Task Update(TEditModel model, bool exceptionRaiseIfNotExist = false);
    }








}
