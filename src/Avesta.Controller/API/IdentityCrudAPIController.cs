using Avesta.Share.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avesta.Controller.API
{
    class IdentityCrudAPIController<TUser, TViewModel, TEditViewModel, TCreateViewModel> : ControllerBase
        where TCreateViewModel : TViewModel
        where TEditViewModel : TViewModel
        where TViewModel : BaseModel
        where TUser : IdentityUser
    {



    }
}
