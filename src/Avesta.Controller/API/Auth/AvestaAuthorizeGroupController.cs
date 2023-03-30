using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Auth.Authorize.Service;
using Avesta.Controller.MVCController;
using Avesta.Data.Model;

namespace Avesta.Controller.API.Auth
{


    public class AvestaAuthorizeGroupController<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
        : Avesta.Controller.API.Crud.CrudController<TId, TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel<TId>, CreateAvestaAuthorizeGroupModel<TId>, EditAvestaAuthorizeGroupModel<TId>>
        where TId : class
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TAvestaUserAuthorizeGroup>
        where TAvestaUser : AvestaUser<TId, TAvestaUserAuthorizeGroup>
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaAuthorizeGroupController(IAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup> authorizeGroupService)
            : base(authorizeGroupService)
        {
        }

    }


}
