using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Auth.Authorize.Model.UserAuthorizeGroup;
using Avesta.Auth.Authorize.Service;
using Avesta.Data.Entity.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthorizeGroupEndPointController = Avesta.Constant.EndPoints.Auth.UserAuthorizeGroupController;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Avesta.Data.Identity.Model;
using Avesta.Data.IdentityCore.Model;

namespace Avesta.Controller.API.Auth
{


    public class AvestaUserAuthorizeGroupController<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
        : Avesta.Controller.API.Crud.CrudController<TId, TAvestaUserAuthorizeGroup, AvestaUserAuthorizeGroupModel<TId>, CreateAvestaUserAuthorizeGroupModel<TId>, EditAvestaUserAuthorizeGroupModel<TId>>
        where TId : class
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TId, TAvestaUserAuthorizeGroup>
        where TAvestaUser : AvestaUser<TId, TAvestaUserAuthorizeGroup>
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup<TId>
    {
        public AvestaUserAuthorizeGroupController(IAvestaUserAuthorizeGroupService<TId, TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup> avestaUserAuthorizeGroupService)
            : base(avestaUserAuthorizeGroupService)
        {
        }
    }


}
