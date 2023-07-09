using AutoMapper;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository.Delete;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Entity.Delete
{
    public class DeleteEntityService<TId, TEntity, TModel> : BaseEntityService, IDeleteEntityService<TId, TEntity, TModel>
         where TId : class
         where TEntity : BaseEntity<TId>
         where TModel : BaseModel<TId>
    {
        readonly IDeleteRepository<TEntity, TId> _deleteRepository;
        readonly IMapper _mapper;
        public DeleteEntityService(IDeleteRepository<TEntity, TId> deleteRepository, IMapper mapper)
        {
            _deleteRepository = deleteRepository;
            _mapper = mapper;
        }


        public async Task Delete(TModel model, bool exceptionRaiseIfNotExist = false)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _deleteRepository.Delete(entity, exceptionRaiseIfNotExist);
        }

        public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.Delete(id, exceptionRaiseIfNotExist);
        }

        public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.Delete(single, exceptionRaiseIfNotExist);
        }

        public async Task DeleteRange(IEnumerable<TModel> models)
        {
            var entities = _mapper.Map<IEnumerable<TEntity>>(models);
            await _deleteRepository.DeleteRange(entities);
        }

        public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
        {
            await _deleteRepository.DeleteRange(where);
        }

        public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
        {
            await _deleteRepository.SoftDelete(id, exceptionRaiseIfNotExist);
        }
    }

}
