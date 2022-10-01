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


    public interface IUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup>
    : ICrudService<TAvestaAuthorizeGroup, UserAuthorizeGroupModel, EditUserAuthorizeGroupModel, CreateUserAuthorizeGroupModel>
    where TAvestaUser : AvestaUser
    where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
    {
    }



    public class UserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup>
         : EntityService<TAvestaAuthorizeGroup, UserAuthorizeGroupModel, EditUserAuthorizeGroupModel, CreateUserAuthorizeGroupModel>, IUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup>
        where TAvestaUser : AvestaUser
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
    {
        public UserAuthorizeGroupService(IRepository<TAvestaAuthorizeGroup> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }


}
