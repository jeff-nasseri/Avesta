using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Data.Identity.Model;
using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using Avesta.Services;
using Avesta.Services.Entity.Availability;
using Avesta.Services.Entity.Create;
using Avesta.Services.Entity.Delete;
using Avesta.Services.Entity.Graph;
using Avesta.Services.Entity.Read;
using Avesta.Services.Entity.Update;
using Avesta.Services.Entity;

namespace Avesta.Auth.Authorize.Service
{



    public interface IAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
            : IEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, CreateAvestaAuthorizeGroupModel<TId>, EditAvestaAuthorizeGroupModel<TId>>
        where TId : class
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
        where TAvestaUser : AvestaUser<TId, TUserAuthorizeGroup>
    {
        Task<string?> GetFeatureStrOfGroup(TId groupId);
    }


    public class AuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
            : EntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, CreateAvestaAuthorizeGroupModel<TId>, EditAvestaAuthorizeGroupModel<TId>>
            , IAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
        where TId : class
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
        where TAvestaUser : AvestaUser<TId, TUserAuthorizeGroup>
    {
        public AuthorizeGroupService(IReadEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>> readEntityService
            , IUpdateEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, EditAvestaAuthorizeGroupModel<TId>> updateEntityService
            , IDeleteEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>> deleteEntityService
            , IAvailabilityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>> availabilityService
            , ICreateEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, CreateAvestaAuthorizeGroupModel<TId>> createEntityService) 
                : base(readEntityService, updateEntityService, deleteEntityService, availabilityService, createEntityService)
        {
        }

        public async Task<string?> GetFeatureStrOfGroup(TId groupId)
        {
            var group = await base.Get(groupId, includeAllPath: false, exceptionRaiseIfNotExist: true);
            return group.AccessStr;
        }


    }



}
