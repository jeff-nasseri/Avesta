using AutoMapper;
using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Auth.User.Service;
using Avesta.Data.Model;
using Avesta.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Authorize.Service
{



    public interface IAuthorizeGroupService<TAvestaAuthorizeGroup> : ICrudService<TAvestaAuthorizeGroup, AuthorizeGroupModel, EditAuthorizeGroupModel, CreateAuthorizeGroupModel>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
    {
    }


    public class AuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup>
        : EntityService<TAvestaAuthorizeGroup, AuthorizeGroupModel, EditAuthorizeGroupModel, CreateAuthorizeGroupModel>, IAuthorizeGroupService<TAvestaAuthorizeGroup>
        where TAvestaUser : AvestaUser
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
    {
        public AuthorizeGroupService(IRepository<TAvestaAuthorizeGroup> repository
            , IMapper mapper) : base(repository, mapper)
        {
        }
    }



}
