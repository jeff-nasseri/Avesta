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


    public interface IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
           : ICrudService<TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>

        where TAvestaUser : AvestaUser<TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
    }



    public class AvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
             : EntityService<TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>
         , IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>

        where TAvestaUser : AvestaUser<TUserAuthorizeGroup>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AvestaUserAuthorizeGroupService(IRepository<TUserAuthorizeGroup> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
