using AutoMapper;
using Avesta.Auth.Authorize.Model.UserAuthorizeGroup;
using Avesta.Data.Identity.Model;
using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using Avesta.Repository.EntityRepository;
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


    public interface IAvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
           : IEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, CreateAvestaUserAuthorizeGroupModel<TId>, EditAvestaUserAuthorizeGroupModel<TId>>
        where TId : class
        where TAvestaUser : AvestaUser<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
    }



    public class AvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
             : EntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, CreateAvestaUserAuthorizeGroupModel<TId>, EditAvestaUserAuthorizeGroupModel<TId>>
         , IAvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TUserAuthorizeGroup>
        where TId : class
        where TAvestaUser : AvestaUser<TId, TUserAuthorizeGroup>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TUserAuthorizeGroup>
        where TUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaUserAuthorizeGroupService(IReadEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>> readEntityService
            , IUpdateEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>
                , EditAvestaUserAuthorizeGroupModel<TId>> updateEntityService
            , IDeleteEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>> deleteEntityService
            , IAvailabilityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>> availabilityService
            , ICreateEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, CreateAvestaUserAuthorizeGroupModel<TId>> createEntityService) 
                : base(readEntityService, updateEntityService, deleteEntityService, availabilityService, createEntityService)
        {
        }
    }


}
