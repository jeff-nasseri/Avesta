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



    public interface IAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup> 
            : ICrudService<TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel, EditAvestaAuthorizeGroupModel, CreateAvestaAuthorizeGroupModel>

        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup
        where TAvestaUser : AvestaUser<TUserAuthorizeGroup>
    {
        Task<string?> GetFeatureStrOfGroup(string groupId);
    }


    public class AuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
            : EntityService<TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel, EditAvestaAuthorizeGroupModel, CreateAvestaAuthorizeGroupModel>
            , IAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>

        where TAvestaUser : AvestaUser<TUserAuthorizeGroup>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AuthorizeGroupService(IRepository<TAvestaAuthorizeGroup> repository
            , IMapper mapper) : base(repository, mapper)
        {
        }


        public async Task<string?> GetFeatureStrOfGroup(string groupId)
        {
            var group = await base.Get(groupId, exceptionRaseIfNotExist: true);
            return group.FeaturesStr;
        }


    }



}
