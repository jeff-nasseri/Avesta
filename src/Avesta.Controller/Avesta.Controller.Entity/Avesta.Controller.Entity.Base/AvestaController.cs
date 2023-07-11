using Avesta.Share.Model.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;

namespace Avesta.Controller.Entity.Base
{



    public class AvestaBaseController : Microsoft.AspNetCore.Mvc.Controller
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public override OkObjectResult Ok(object data)
        {
            var response = new ResponseModel().Success(data);
            return base.Ok(response);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public override BadRequestObjectResult BadRequest(/*[ActionResultObjectValue]*/ object error)
        {
            var response = new ResponseModel().Fail(error);
            return base.BadRequest(response);
        }


    }


}
