using AutoMapper;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Create
{
    public interface ICreateEntityService<TId, TEntity, TModel, TCreateModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
        where TCreateModel : TModel
    {

        Task ClearAllEntitiesThenAddRange(IEnumerable<TModel> insertModels);
        Task ClearRemoveListThenAddRange(IEnumerable<TModel> removeList, IEnumerable<TModel> insertModels);
        Task Insert(TModel model);
        Task InsertRange(IEnumerable<TModel> models);
        Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TModel> insertModels);


    }






}
