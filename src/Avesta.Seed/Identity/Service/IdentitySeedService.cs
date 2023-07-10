using Avesta.Data.IdentityCore.Model;
using Avesta.Data.Entity.Model;
using Avesta.Seed.Identity.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avesta.Repository.IdentityCore;

namespace Avesta.Seed.Identity.Service
{

    public interface IIdentitySeedService<TAvestaUser, TRole>
        where TAvestaUser : AvestaIdentityUser
        where TRole : IdentityRole
    {
        Task<IdentityResult> Seed(AvestaUserSeedModel<TAvestaUser> model);
        Task<IEnumerable<IdentityResult>> SeedRange(IEnumerable<AvestaUserSeedModel<TAvestaUser>> models);


    }



    public class IdentitySeedService<TAvestaUser, TRole> : IIdentitySeedService<TAvestaUser, TRole>
        where TAvestaUser : AvestaIdentityUser
        where TRole : IdentityRole
    {

        readonly IIdentityRepository<TAvestaUser, TRole> _identityRepository;
        public IdentitySeedService(IIdentityRepository<TAvestaUser, TRole> identityRepository)
        {
            _identityRepository = identityRepository;
        }


        public async Task<IdentityResult> Seed(AvestaUserSeedModel<TAvestaUser> model)
        {
            var result = await _identityRepository.RegisterNewUser(model.User, model.Password);
            return result;
        }

        public async Task<IEnumerable<IdentityResult>> SeedRange(IEnumerable<AvestaUserSeedModel<TAvestaUser>> models)
        {
            var list = new List<IdentityResult>();
            foreach (var model in models)
            {
                var result = await Seed(model);
                list.Add(result);
            }
            return list;
        }




    }




}
