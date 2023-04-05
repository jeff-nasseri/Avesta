using Avesta.Attribute.Controller;
using Avesta.Controller;
using Avesta.Graph.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Avesta.Graph.Controller
{

    [AvestaMVCController]
    [Route("/Avesta/Graph")]
    public class AvestaGraphMVCController : AvestaBaseController
    {
        readonly IGraphHierarchySrevice _graphHierarchySrevice;
        public AvestaGraphMVCController(IGraphHierarchySrevice graphHierarchySrevice)
        {
            _graphHierarchySrevice = graphHierarchySrevice;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await Task.CompletedTask;
            return Content("<h1>hello</h1>", "text/html; charset=UTF-8");
        }



        [Route("graph.json")]
        [HttpGet]
        public async Task<string> GetGraphEntities()
        {
            var data = await _graphHierarchySrevice.GetHierarchyOfCurrentExecuteableApplication();
            var result = JsonConvert.SerializeObject(data);
            return result;
        }

    }



}
