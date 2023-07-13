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

    public interface IIdentitySeedService<TId, TAvestaUser, TRole>
        where TId : class, IEquatable<TId>
        where TAvestaUser : AvestaIdentityUser<TId>
        where TRole : IdentityRole
    {
        Task<IdentityResult> Seed(AvestaUserSeedModel<TId, TAvestaUser> model);
        Task<IEnumerable<IdentityResult>> SeedRange(IEnumerable<AvestaUserSeedModel<TId, TAvestaUser>> models);


    }


    public class IdentitySeedService<TId, TAvestaUser, TRole> : IIdentitySeedService<TId, TAvestaUser, TRole>
        where TId : class, IEquatable<TId>
        where TAvestaUser : AvestaIdentityUser<TId>
        where TRole : IdentityRole
    {

        readonly IIdentityRepository<TId, TAvestaUser, TRole> _identityRepository;
        public IdentitySeedService(IIdentityRepository<TId, TAvestaUser, TRole> identityRepository)
        {
            _identityRepository = identityRepository;
        }


        public async Task<IdentityResult> Seed(AvestaUserSeedModel<TId, TAvestaUser> model)
        {
            var result = await _identityRepository.RegisterNewUser(model.User, model.Password);
            return result;
        }

        public async Task<IEnumerable<IdentityResult>> SeedRange(IEnumerable<AvestaUserSeedModel<TId, TAvestaUser>> models)
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
