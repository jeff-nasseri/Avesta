using Avesta.Controller.API;
using AvestaWebAPI.Data;
using AvestaWebAPI.Model;
using AvestaWebAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AvestaWebAPI.Controllers
{
    [ApiController]
    [Route("/crud")]
    public class CustomeAvestaController : CrudAPIController<AvestaCrudEntity, AvestaCrudModel, EditAvestaCrudModel, CreateAvestaCrudModel>
    {
        public CustomeAvestaController(IAvestaCrudService avestaCrudService) : base(avestaCrudService)
        {
        }
    }
}
