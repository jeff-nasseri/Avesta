﻿using AutoMapper;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Update;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Update
{
    public interface IEntityUpdateService<TId, TEntity, TModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
    {

        Task Update(TModel model, bool exceptionRaiseIfNotExist = false);

    }


    public interface IEntityUpdateService<TId, TEntity, TModel, TEditModel> : IEntityUpdateService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TEditModel : TModel
    {
        Task Update(TEditModel model, bool exceptionRaiseIfNotExist = false);
    }








}
