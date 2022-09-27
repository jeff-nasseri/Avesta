using Avesta.Data.Model;
using Avesta.Seed.Entity.Service;
using Avesta.Seed.Identity.Model;
using Avesta.Seed.Identity.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeedEndPointController = Avesta.Storage.Constant.EndPoints.SeedController;

namespace Avesta.MVC.Seed.API
{

    public interface ISeedController<TAvestaUser>
        where TAvestaUser : AvestaUser
    {
        Task<IActionResult> SeedUsers(AvestaUserSeedModel<TAvestaUser> model);
        Task<IActionResult> SeedTable(string tableName);

    }

    public class SeedController<TAvestaUser, TRole> : ControllerBase, ISeedController<TAvestaUser>
        where TAvestaUser : AvestaUser
        where TRole : IdentityRole
    {
        readonly IEntitySeedService _entitySeedService;
        readonly IIdentitySeedService<TAvestaUser, TRole> _identitySeedService;
        public SeedController(IEntitySeedService entitySeedService, IIdentitySeedService<TAvestaUser, TRole> identitySeedService)
        {
            _entitySeedService = entitySeedService;
            _identitySeedService = identitySeedService;
        }


        [HttpPost]
        [Route(SeedEndPointController.SeedTable)]
        public async Task<IActionResult> SeedTable(string tableName)
        {
            throw new NotImplementedException();
        }



        [HttpPost]
        [Route(SeedEndPointController.SeedUsers)]
        public async Task<IActionResult> SeedUsers(AvestaUserSeedModel<TAvestaUser> model)
        {
            if (ModelState.IsValid)
            {
                var result = await _identitySeedService.Seed(model);
                return Ok(result);
            }
            return BadRequest(model);
        }


    }
}
