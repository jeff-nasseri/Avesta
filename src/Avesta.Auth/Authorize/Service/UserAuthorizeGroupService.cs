using AutoMapper;
using Avesta.Auth.Authorize.Model.UserAuthorizeGroup;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authorize.Service
{


    public interface IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaUserAuthorizeGroup>
    : ICrudService<TAvestaUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>
    where TAvestaUser : AvestaUser
    where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
    }



    public class AvestaUserAuthorizeGroupService<TAvestaUser, TAvestaUserAuthorizeGroup>
         : EntityService<TAvestaUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>
        , IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaUserAuthorizeGroup>
        where TAvestaUser : AvestaUser
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AvestaUserAuthorizeGroupService(IRepository<TAvestaUserAuthorizeGroup> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
