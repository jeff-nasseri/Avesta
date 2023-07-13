using Avesta.Auth.Identity.Model;
using Avesta.Data.Identity.Model;
using Avesta.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Auth.Identity
{
    public class AvestaUserManager<TAvestaUser> : AvestaUserManager<string, TAvestaUser>
         where TAvestaUser : AvestaUser<string>
    {
        public AvestaUserManager(IEntityRepository<TAvestaUser, string> userRepository
            , IEntityRepository<AvestaAuthorizeGroup<string, AvestaUserAuthorizeGroup<string, TAvestaUser>, TAvestaUser>, string> authorizeGroupRepository
            , IEntityRepository<AvestaUserAuthorizeGroup<string, TAvestaUser>, string> userAuthorizeGroupRepository
            , IEntityRepository<AvestaUserActivity<string, TAvestaUser>, string> userActivitiesRepository
            , IEntityRepository<AvestaUserToken<string, TAvestaUser>, string> userTokensRepository) 
                : base(userRepository, authorizeGroupRepository, userAuthorizeGroupRepository, userActivitiesRepository, userTokensRepository)
        {
        }
    }



    public class AvestaUserManager<TId, TAvestaUser> : AvestaUserHandler<TId
         , TAvestaUser
         , AvestaAuthorizeGroup<TId, AvestaUserAuthorizeGroup<TId, TAvestaUser>, TAvestaUser>
         , AvestaUserAuthorizeGroup<TId, TAvestaUser>
         , AvestaUserToken<TId, TAvestaUser>
         , AvestaUserActivity<TId, TAvestaUser>>
        where TId : class
        where TAvestaUser : AvestaUser<TId>
    {
        public AvestaUserManager(IEntityRepository<TAvestaUser, TId> userRepository
            , IEntityRepository<AvestaAuthorizeGroup<TId, AvestaUserAuthorizeGroup<TId, TAvestaUser>, TAvestaUser>, TId> authorizeGroupRepository
            , IEntityRepository<AvestaUserAuthorizeGroup<TId, TAvestaUser>, TId> userAuthorizeGroupRepository
            , IEntityRepository<AvestaUserActivity<TId, TAvestaUser>, TId> userActivitiesRepository
            , IEntityRepository<AvestaUserToken<TId, TAvestaUser>, TId> userTokensRepository)
                : base(userRepository, authorizeGroupRepository, userAuthorizeGroupRepository, userActivitiesRepository, userTokensRepository)
        {
        }

        public override Task<AvestaIdentityResult> AddNewAuthorizeGroup(string name, IEnumerable<string> acess)
        {
            throw new NotImplementedException();
        }

        public override Task<AvestaIdentityResult> AddUserToExistingAuthorizeGroupByGroupId(TAvestaUser user, TId id)
        {
            throw new NotImplementedException();
        }

        public override Task<AvestaUserToken<TId, TAvestaUser>> GenerateToken(TAvestaUser user)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GenerateToken()
        {
            throw new NotImplementedException();
        }
    }

}
