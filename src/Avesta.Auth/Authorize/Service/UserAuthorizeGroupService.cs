using AutoMapper;
using Avesta.Auth.Authorize.Model.UserAuthorizeGroup;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Services;
using Avesta.Services.Create;
using Avesta.Services.Delete;
using Avesta.Services.Graph;
using Avesta.Services.Read;
using Avesta.Services.Update;

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
            , IUpdateEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, EditAvestaUserAuthorizeGroupModel<TId>> updateEntityService
            , IDeleteEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>> deleteEntityService
            , IEntityGraphService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>> entityGraphService
            , ICreateEntityService<TId, TUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, CreateAvestaUserAuthorizeGroupModel<TId>> createEntityService) 
                : base(readEntityService, updateEntityService, deleteEntityService, entityGraphService, createEntityService)
        {
        }
    }


}
