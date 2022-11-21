using AutoMapper;
using Avesta.Repository.EntityRepository;
using Avesta.Services;
using AvestaWebAPI.Data;
using AvestaWebAPI.Model;

namespace AvestaWebAPI.Service
{

    public interface IAvestaCrudService : ICrudService<AvestaCrudEntity, AvestaCrudModel, EditAvestaCrudModel, CreateAvestaCrudModel>
    {
    }

    public class AvestaCrudService : EntityService<AvestaCrudEntity, AvestaCrudModel, EditAvestaCrudModel, CreateAvestaCrudModel>, IAvestaCrudService
    {
        public AvestaCrudService(IRepository<AvestaCrudEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
