using Avesta.Data.Model;
using Avesta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services
{
    public interface ICrudServices<TModel, TViewModel, TEditViewModel, TCreateViewModel> : IBaseService<TModel>, IBaseCrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel>
        where TModel : BaseEntity
        where TViewModel : BaseModel
        where TEditViewModel : TViewModel
        where TCreateViewModel : TViewModel
    {
       
    }




    public interface IBaseCrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel>
        where TModel : class
        where TViewModel : BaseModel
        where TEditViewModel : TViewModel
        where TCreateViewModel : TViewModel
    {
        Task<IEnumerable<TModel>> GetAllEntities();
        Task<IEnumerable<TModel>> GetAllEntitiesWithAllChildren();
        Task<TModel> GetEntityWithAllChildren(string id);
        Task<IEnumerable<TViewModel>> GetAllEntitiesAsViewModel();
        Task<IEnumerable<TModel>> GetAllEntities(string navigationProperties);
        Task CreateNew(TCreateViewModel viewModel);
        Task<IEnumerable<TViewModel>> GetAllByParentInfo(ParentInfo info);
        Task CreateNew(TModel model);
        Task<TEditViewModel> GetEntityAsViewModel(string id, bool exceptionRaseIfNotExist = false);
        Task<TModel> GetEntity(string id, bool exceptionRaseIfNotExist);
        Task<TModel> GetEntity(string navigationProperties, string id, bool exceptionRaseIfNotExist);
        Task EditEntity(TEditViewModel viewModel);
        Task Delete(string id);
        Task SoftDelete(string id, bool exceptionRaseIfNotExist = true);
        Task<IEnumerable<TModel>> GetLastN(int n);
        Task<IEnumerable<TModel>> Where(string navigationProperties, Expression<Func<TModel, bool>> where);
        Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> where);
        Task<IEnumerable<TModel>> GetLastN<TKey>(int n, Expression<Func<TModel, TKey>> orderBy) where TKey : class;
        Task<IEnumerable<TModel>> GetLastN(int n, Expression<Func<TModel, bool>> filter);
    }
}
