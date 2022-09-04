//using AutoMapper;
//using Avesta.Model;
//using Avesta.Repository.Identity;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace Avesta.Services
//{
//    public class IdentityService<TUser, TRole, TUserViewModel>
//        where TUser : IdentityUser
//        where TRole : IdentityRole
//        where TUserViewModel : BaseModel
//    {
//        readonly IIdentityRepository<TUser, TRole> _identityRepository;
//        readonly IMapper _mapper;
//        public IdentityService(IIdentityRepository<TUser, TRole> identityRepository, IMapper mapper)
//        {
//            _identityRepository = identityRepository;
//            _mapper = mapper;
//        }

//        public virtual async Task<IEnumerable<TUser>> GetAllEntitiesWithAllChildren()
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task Detach(TUser model)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task<IEnumerable<TUser>> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task<IEnumerable<TUser>> GetAll(string navigationPropertyPath)
//        {
//            throw new NotImplementedException();
//        }
//        public virtual async Task<IEnumerable<TUser>> GetAll(string navigationPropertyPath, Expression<Func<TUser, bool>> exp)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task<IEnumerable<TUserViewModel>> GetAllAsViewModel()
//        {
//            throw new NotImplementedException();
//        }
//        public virtual async Task<IEnumerable<TUserViewModel>> GetAllAsViewModel(Expression<Func<TUser, bool>> exp)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task Create(TUserViewModel viewModel)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task<IEnumerable<TUserViewModel>> GetAllByParentInfo(ParentInfo info)
//        {
//            throw new NotImplementedException();
//        }

//        public virtual async Task Create(TUser model)
//        {
//            throw new NotImplementedException();
//        }



//        public virtual async Task<IEnumerable<TUser>> GetAll(Expression<Func<TUser, bool>> exp)
//        {
//            throw new NotImplementedException();
//        }
//        public virtual async Task<TUser> Get(Expression<Func<TUser, bool>> exp, bool exceptionRaseIfNotExist)
//        {
//            var result = await _identityRepository.GetEntity(exp, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            return result;
//        }
//        public virtual async Task<TUser> Get(string navigationProperties, Expression<Func<TUser, bool>> exp, bool exceptionRaseIfNotExist)
//        {
//            var result = await _identityRepository.GetEntity(navigationProperties, exp, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            return result;
//        }
//        public virtual async Task<TUser> Get(string id, bool exceptionRaseIfNotExist)
//        {
//            var result = await _identityRepository.GetByIdAsync(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            return result;
//        }
//        public virtual async Task<TUserViewModel> GetAsViewModel(string id, bool exceptionRaseIfNotExist)
//        {
//            var entity = await _identityRepository.GetByIdAsync(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            var model = _mapper.Map<TUserViewModel>(entity);
//            return model;
//        }
//        public virtual async Task<GViewModel> GetAsViewModel<GViewModel>(string id, bool exceptionRaseIfNotExist) where GViewModel : BaseModel
//        {
//            var TUserViewModel = await GetAsViewModel(id, exceptionRaseIfNotExist);
//            var model = _mapper.Map<GViewModel>(TUserViewModel);
//            return model;
//        }
//        public virtual async Task<TUserViewModel> GetAsViewModel(string navigationProperties, Expression<Func<TUser, bool>> exp, bool exceptionRaseIfNotExist)
//        {
//            var entity = await _identityRepository.GetEntity(navigationProperties, exp, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            var model = _mapper.Map<TUserViewModel>(entity);
//            return model;
//        }



//        public virtual async Task DeleteById(string navigationProperties, string id)
//        {
//            var entity = await Get(navigationProperties, e => e.ID == id, exceptionRaseIfNotExist: true);
//            await Delete(entity);
//        }
//        public virtual async Task DeleteById(string id)
//        {
//            await _identityRepository.DeleteWithAllChildren(id);
//        }
//        public virtual async Task SoftDeleteById(string id, bool exceptionRaseIfNotExist)
//        {
//            await _identityRepository.SoftDelete(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//        }
//        public virtual async Task Delete(TUser model)
//        {
//            await _identityRepository.Delete(model);
//        }


//        public virtual async Task Update(TUserViewModel viewModel)
//        {
//            var entity = _mapper.Map<TUser>(viewModel);
//            await Update(entity);
//        }
//        public virtual async Task Update(TUser model)
//        {
//            await _identityRepository.UpdateAsync(model);
//        }
//        public virtual async Task Update<SourceViewModel>(SourceViewModel sourceViewModel)
//        {
//            var viewModel = _mapper.Map<TUserViewModel>(sourceViewModel);
//            await Update(viewModel);
//        }

//        public virtual async Task<IEnumerable<TUser>> Search(string keywords)
//        {
//            var all = await GetAll();
//            var result = await all.Search(keywords);
//            return result;
//        }


//    }


//    public interface IIdentityService<TUser, TRole, TUserViewModel, TEditUserViewModel, TCreateUserViewModel> : IBaseCrudService<TUser, TUserViewModel, TEditUserViewModel, TCreateUserViewModel>
//        where TUser : IdentityUser
//        where TRole : IdentityRole
//        where TUserViewModel : BaseModel
//        where TEditUserViewModel : TUserViewModel
//        where TCreateUserViewModel : TUserViewModel
//    {
//    }

//    class IdentityService<TUser, TRole, TUserViewModel, TEditUserViewModel, TCreateUserViewModel> : IdentityService<TUser, TRole, TUserViewModel>
//        , IBaseService<TUser>
//        , IIdentityService<TUser, TRole, TUserViewModel, TEditUserViewModel, TCreateUserViewModel>
//        where TUser : IdentityUser
//        where TRole : IdentityRole
//        where TUserViewModel : BaseModel
//        where TEditUserViewModel : TUserViewModel
//        where TCreateUserViewModel : TUserViewModel
//    {
//        public IdentityService(IIdentityRepository<TUser, TRole> identityRepository, IMapper mapper) : base(identityRepository, mapper)
//        {
//        }

//        public async Task<IEnumerable<TUser>> GetLastN(int n)
//        {
//            var entities = await GetAllEntities();
//            var result = entities.TakeLast(n).ToList();
//            return result;
//        }

//        public async Task<IEnumerable<TUser>> Where(Expression<Func<TUser, bool>> where)
//        {
//            var result = await base.GetAll(exp: where);
//            return result;
//        }

//        public async Task<IEnumerable<TUser>> Where(string navigationProperties, Expression<Func<TUser, bool>> where)
//        {
//            var result = await base.GetAll(navigationProperties, exp: where);
//            return result;
//        }

//        public async Task<IEnumerable<TUser>> GetLastN<TKey>(int n, Expression<Func<TUser, TKey>> orderBy) where TKey : class
//        {
//            var entities = await GetAllEntities();
//            var result = entities.AsQueryable().OrderBy(orderBy).TakeLast(n).ToList();
//            return result;
//        }

//        public async Task<IEnumerable<TUser>> GetLastN(int n, Expression<Func<TUser, bool>> filter)
//        {
//            var entities = await GetAllEntities();
//            var result = entities.AsQueryable().Where(filter).TakeLast(n).ToList();
//            return result;
//        }


//        public async Task<TUser> GetEntity(string id, bool exceptionRaseIfNotExist)
//        {
//            return await base.Get(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//        }

//        public virtual async Task<TUser> GetEntity(string navigationProperties, string id, bool exceptionRaseIfNotExist)
//        {
//            var result = await base.Get(navigationProperties, i => i.ID == id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//            return result;
//        }


//        public virtual async Task CreateNew(TCreateViewUser viewUser)
//        {
//            await base.Create(viewUser);
//        }


//        public virtual async Task CreateNew(TUser User)
//        {
//            await base.Create(User);
//        }
//        public virtual async Task Delete(string id)
//        {
//            await base.DeleteById(id);
//        }
//        public virtual async Task SoftDelete(string id, bool exceptionRaseIfNotExist = true)
//        {
//            await base.SoftDeleteById(id, exceptionRaseIfNotExist: exceptionRaseIfNotExist);
//        }



//        public virtual async Task EditEntity(TEditViewUser viewUser)
//        {
//            await base.Update(viewUser);
//        }

//        public virtual async Task<IEnumerable<TUser>> GetAllEntities()
//        {
//            var result = await base.GetAll();
//            return result.OrderByDescending(e => e.CreateDate).ToList();
//        }

//        public virtual async Task<IEnumerable<TUser>> GetAllEntitiesIncludeAllChildren()
//        {
//            var result = await base.GetAllEntitiesWithAllChildren();
//            return result;
//        }


//        public async Task<IEnumerable<TViewUser>> GetAllEntitiesAsViewUser()
//        {
//            var result = await base.GetAllAsViewUser();
//            return result.ToList();
//        }


//        public virtual async Task<IEnumerable<TUser>> GetAllEntities(string navigationProperties)
//        {
//            var result = await base.GetAll(navigationProperties);
//            return result.OrderByDescending(e => e.CreateDate).ToList();
//        }

//        public virtual async Task<TEditViewUser> GetEntityAsViewUser(string id, bool exceptionRaseIfNotExist)
//        {
//            var result = await base.GetAsViewUser<TEditViewUser>(id, exceptionRaseIfNotExist);
//            return result;
//        }

//        public virtual async Task<(IEnumerable<TUser>, int)> Paginate(int page, int perPage = Pagination.PerPage, string searchKeyWord = null)
//        {
//            IEnumerable<TUser> resultSearchByCustome = default;
//            IEnumerable<TUser> resultSearchByDefault = default;
//            IEnumerable<TUser> total = null;
//            var all = await GetAllEntities();
//            if (!string.IsNullOrEmpty(searchKeyWord))
//            {
//                resultSearchByCustome = await Search(searchKeyWord);
//                resultSearchByDefault = await all.Search(keyword: searchKeyWord);
//                total = List.Merge(resultSearchByCustome, resultSearchByDefault);
//                total = total.Distinct().ToList();
//            }


//            var result = await (total ?? all).Paginate(page, perPage: perPage);


//            return result;
//        }


//    }
//}
