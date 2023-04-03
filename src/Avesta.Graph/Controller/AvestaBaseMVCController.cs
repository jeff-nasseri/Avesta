using Avesta.Attribute.Controller;
using Avesta.Controller;
using Avesta.Graph.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = await _graphHierarchySrevice.GetHierarchyOfCurrentExecuteableApplication();
            return View(result);
        }
    }


}
