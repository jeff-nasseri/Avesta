using AutoMapper;
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
    public class EntityUpdateService<TId, TEntity, TModel> : IEntityUpdateService<TId, TEntity, TModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
    {

        readonly IUpdateRepository<TEntity, TId> _updateRepository;
        readonly IMapper _mapper;
        public EntityUpdateService(IUpdateRepository<TEntity, TId> updateRepository, IMapper mapper)
        {
            _updateRepository = updateRepository;
            _mapper = mapper;
        }



        public async Task Update(TModel model, bool exceptionRaiseIfNotExist = false)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _updateRepository.Update(entity, exceptionRaiseIfNotExist);
        }

    }


    public class EntityUpdateService<TId, TEntity, TModel, TEditModel> : EntityUpdateService<TId, TEntity, TModel>, IEntityUpdateService<TId, TEntity, TModel, TEditModel>
       where TId : class
       where TEntity : BaseEntity<TId>
       where TModel : BaseModel<TId>
       where TEditModel : TModel
    {

        public EntityUpdateService(IUpdateRepository<TEntity, TId> updateRepository, IMapper mapper) : base(updateRepository, mapper)
        {
        }

        public async Task Update(TEditModel model, bool exceptionRaiseIfNotExist = false)
            => await base.Update(model, exceptionRaiseIfNotExist);


    }

}
