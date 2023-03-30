using Avesta.Data.Model;
using Avesta.Repository.EntityRepository.Availability;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Services.Availability
{
    public class AvailabilityService<TId, TEntity, TModel> : IAvailabilityService<TId, TEntity, TModel>
          where TId : class
          where TEntity : BaseEntity<TId>
          where TModel : BaseModel<TId>
    {

        readonly IAvailabilityRepository<TEntity, TId> _availabilityRepository;
        public AvailabilityService(IAvailabilityRepository<TEntity, TId> availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public Task<bool> Any(TModel model, string navigationPropertyPath = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Any(TId id, string navigationPropertyPath = null)
            => await _availabilityRepository.Any(e => e.ID == id, navigationPropertyPath);

        public async Task<bool> Any(Expression<Func<TEntity, bool>> expression, string navigationPropertyPath = null)
            => await _availabilityRepository.Any(expression, navigationPropertyPath);

        public Task CheckAvailability(TModel model, string navigationPropertyPath = null)
        {
            throw new NotImplementedException();
        }

        public async Task CheckAvailability(TId id, string navigationPropertyPath = null)
            => await _availabilityRepository.CheckAvailability(e => e.ID == id, navigationPropertyPath);

        public async Task CheckAvailability(Expression<Func<TEntity, bool>> expression, string navigationPropertyPath = null)
            => await _availabilityRepository.Any(expression, navigationPropertyPath);

    }
}
