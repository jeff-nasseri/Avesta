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
using UserAuthorizeGroupEndPointController = Avesta.Constant.EndPoints.Auth.UserAuthorizeGroupController;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Avesta.Controller.API.Auth
{


    public class AvestaUserAuthorizeGroupController<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup>
        : CrudAPIController<TAvestaUserAuthorizeGroup, AvestaUserAuthorizeGroupModel, EditAvestaUserAuthorizeGroupModel, CreateAvestaUserAuthorizeGroupModel>
        where TAvestaAuthorizeGroup : AvestaAuthorizeGroup<TAvestaUserAuthorizeGroup>
        where TAvestaUser : AvestaUser<TAvestaUserAuthorizeGroup>
        where TAvestaUserAuthorizeGroup : AvestaUserAuthorizeGroup
    {
        public AvestaUserAuthorizeGroupController(IAvestaUserAuthorizeGroupService<TAvestaUser, TAvestaAuthorizeGroup, TAvestaUserAuthorizeGroup> avestaUserAuthorizeGroupService)
            : base(avestaUserAuthorizeGroupService)
        {
        }
    }


}
