using Avesta.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.MVC
{
    class IdentityCrudAPIController<TUser, TViewModel, TEditViewModel, TCreateViewModel> : ControllerBase
        where TCreateViewModel : TViewModel
        where TEditViewModel : TViewModel
        where TViewModel : BaseModel
        where TUser : IdentityUser
    {



    }
}
