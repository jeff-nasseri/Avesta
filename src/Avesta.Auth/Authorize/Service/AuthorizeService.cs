using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Data.Model;
using Avesta.Services;
using Avesta.Services.Availability;
using Avesta.Services.Create;
using Avesta.Services.Delete;
using Avesta.Services.Graph;
using Avesta.Services.Read;
using Avesta.Services.Update;

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
            , IEntityGraphService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>> entityGraphService
            , IAvailabilityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>> availabilityService
            , ICreateEntityService<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, CreateAvestaAuthorizeGroupModel<TId>> createEntityService) 
                : base(readEntityService, updateEntityService, deleteEntityService, entityGraphService, availabilityService, createEntityService)
        {
        }

        public async Task<string?> GetFeatureStrOfGroup(TId groupId)
        {
            var group = await base.Get(groupId, includeAllPath: false, exceptionRaiseIfNotExist: true);
            return group.AccessStr;
        }


    }



}
