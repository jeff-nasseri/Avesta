using Avesta.Auth.Authorize.Model.AuthorizeGroup;
using Avesta.Auth.Authorize.Service;
using Avesta.Data.Model;
using Avesta.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeGroupEndPointController = Avesta.Storage.Constant.EndPoints.Auth.AuthorizeGroupController;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Avesta.Controller.API.Auth
{

    [ApiController]
    [Route(AuthorizeGroupEndPointController.Controller)]
    public class AvestaAuthorizeGroupController<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
        : CrudAPIController<TAvestaAuthorizeGroup, AvestaAuthorizeGroupModel, EditAvestaAuthorizeGroupModel, CreateAvestaAuthorizeGroupModel>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup
        where TAvestaUser : AvestaUser
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AvestaAuthorizeGroupController(IAuthorizeGroupService<TAvestaAuthorizeGroup> authorizeGroupService) : base(authorizeGroupService)
        {   
        }
    }


}
