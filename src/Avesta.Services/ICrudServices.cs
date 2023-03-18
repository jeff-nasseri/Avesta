using Avesta.Data.Model;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Avesta.Services
{
    public interface ICrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel> : IBaseService<TModel, TViewModel>, IBaseCrudService<TModel, TViewModel, TEditViewModel, TCreateViewModel>
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
        Task<IEnumerable<TModel>> GetAllEntities(int skip, int take);
        Task<IEnumerable<TModel>> GetAllEntitiesWithAllChildren();
        Task<IEnumerable<TModel>> GetAllEntitiesWithSpecificChildren(string navigationPropertyPath);
        Task<TModel> GetEntityWithAllChildren(string id);
        Task<TModel> GetEntityWithSpecificChildren(string id, string navigationPropertyPath);
        Task<IEnumerable<TViewModel>> GetAllEntitiesAsViewModel();
        Task<IEnumerable<TModel>> GetAllEntities(string navigationProperties);
        Task CreateNew(TCreateViewModel viewModel);
        Task CreateRange(IEnumerable<TViewModel> viewModels);
        Task<IEnumerable<TViewModel>> GetAllByPropertyInfo(PropertyInformation info);
        Task<TModel> UpdateOrInsert(TEditViewModel editViewModel);
        Task CreateNew(TModel model);
        Task<TEditViewModel> GetEntityAsViewModel(string id, bool exceptionRaiseIfNotExist = false);
        Task<TModel> GetEntity(string id, bool exceptionRaiseIfNotExist);
        Task<TModel> GetEntity(string navigationProperties, string id, bool exceptionRaiseIfNotExist);
        Task EditEntity(TEditViewModel viewModel);
        Task EditEntity(TModel model);
        Task Delete(string id);
        Task SoftDelete(string id, bool exceptionRaiseIfNotExist = true);
        Task<IEnumerable<TModel>> GetLastN(int n);
        Task<IEnumerable<TModel>> Where(string navigationProperties, Expression<Func<TModel, bool>> where);
        Task<TModel> Get(Expression<Func<TModel, bool>> exp, bool exceptionRaiseIfNotExist);
        Task<IEnumerable<TViewModel>> WhereAsViewModel(string navigationProperties, Expression<Func<TModel, bool>> where);
        Task<IEnumerable<TViewModel>> WhereAsViewModel(Expression<Func<TModel, bool>> where);
        Task<IEnumerable<TModel>> WhereEntitiesWithSpecificChildren(string navigationPropertyPath, string dynamicQuery);
        Task<IEnumerable<TModel>> Where(Expression<Func<TModel, bool>> where);
        Task<IEnumerable<TModel>> GetLastN<TKey>(int n, Expression<Func<TModel, TKey>> orderBy) where TKey : class;
        Task<IEnumerable<TModel>> GetLastN(int n, Expression<Func<TModel, bool>> filter);
        Task<IEnumerable<TModel>> SearchByIncludeNavigationPath(string keywords, string? navigation = null
      , bool? navigateAll = null
      , string? dynamicQuery = null
      , DateTime? startDate = null
      , DateTime? endDate = null);


    }
}
