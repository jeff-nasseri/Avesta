using AutoMapper;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository.Create;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Entity.Create
{


    public class CreateEntityService<TId, TEntity, TModel> : BaseEntityService, ICreateEntityService<TId, TEntity, TModel>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TModel : BaseModel<TId>
    {

        readonly ICreateRepository<TEntity, TId> _createRepository;
        readonly IMapper _mapper;
        public CreateEntityService(ICreateRepository<TEntity, TId> createRepository, IMapper mapper) : base()
        {
            _createRepository = createRepository;
            _mapper = mapper;
        }


        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TModel> insertModels)
        {
            var insertEntities = _mapper.Map<IEnumerable<TEntity>>(insertModels);
            await _createRepository.ClearAllEntitiesThenAddRange(insertEntities);
        }

        public async Task ClearRemoveListThenAddRange(IEnumerable<TModel> removeList, IEnumerable<TModel> insertModels)
        {
            var removeEntities = _mapper.Map<IEnumerable<TEntity>>(removeList);
            var insertEntities = _mapper.Map<IEnumerable<TEntity>>(insertModels);

            await _createRepository.ClearRemoveListThenAddRange(removeEntities, insertEntities);
        }

        public async Task Insert(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _createRepository.Insert(entity);
        }

        public async Task InsertRange(IEnumerable<TModel> models)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(models);
            await _createRepository.InsertRange(entities);
        }

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TModel> insertModels)
        {
            var insertEntities = _mapper.Map<IEnumerable<TEntity>>(insertModels);
            await _createRepository.ReCreate(deleteCondition, insertEntities);
        }
    }










    public class CreateEntityService<TId, TEntity, TModel, TCreateModel> : CreateEntityService<TId, TEntity, TModel>, ICreateEntityService<TId, TEntity, TModel, TCreateModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
       where TCreateModel : TModel
    {

        public CreateEntityService(ICreateRepository<TEntity, TId> createRepository, IMapper mapper) : base(createRepository, mapper)
        {
        }


        public async Task ClearAllEntitiesThenAddRange(IEnumerable<TCreateModel> insertModels)
            => await base.ClearAllEntitiesThenAddRange(insertModels);

        public async Task ClearRemoveListThenAddRange(IEnumerable<TModel> removeList, IEnumerable<TCreateModel> insertModels)
            => await base.ClearRemoveListThenAddRange(removeList, insertModels);

        public async Task Insert(TCreateModel model)
            => await base.Insert(model);

        public async Task InsertRange(IEnumerable<TCreateModel> models)
            => await base.InsertRange(models);

        public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TCreateModel> insertModels)
            => await base.ReCreate(deleteCondition, insertModels);



    }







}
