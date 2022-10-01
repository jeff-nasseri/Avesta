using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Auth.Authorize.Model.UserAuthorizeGroup;
using Avesta.Auth.Authorize.Service;
using Avesta.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuthorizeGroupEndPointController = Avesta.Storage.Constant.EndPoints.Auth.UserAuthorizeGroupController;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Avesta.Controller.API.Auth
{

    [ApiController]
    [Route(UserAuthorizeGroupEndPointController.Controller)]
    public class AvestaUserAuthorizeGroupController<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
        : CrudAPIController<TAvestaUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
        where TAvestaUser : AvestaUser
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AvestaUserAuthorizeGroupController(IAvestaUserAuthorizeGroupService<TAvestaUser,TAvestaUserAuthorizeGroup> avestaUserAuthorizeGroupService) 
            : base(avestaUserAuthorizeGroupService)
        {
        }
    }


}
