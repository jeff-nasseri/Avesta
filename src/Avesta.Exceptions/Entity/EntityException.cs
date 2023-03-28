using Avesta.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Exceptions.Entity
{

    public class EntityException : SystemException
    {
        public object? Key { get; set; }
        public int? EntityId { get; set; }
        public string EntityGUID { get; set; }
        public string Predicate { get; set; }
        public EntityException(int entityId, string predicate, int? code = null) : base(code ?? ExceptionConstant.EntityException)
        {
            EntityId = entityId;
            Predicate = predicate;
        }

        public EntityException(object? key, int? code = null) : base(code ?? ExceptionConstant.EntityException)
        {
            Key = key;
        }

    }
    public class CanNotFoundEntityException : EntityException
    {
        public CanNotFoundEntityException(int entityId, string predicate) : base(entityId, predicate, ExceptionConstant.CanNotFoundEntityException)
        {
        }
        public CanNotFoundEntityException(object? key) : base(key, ExceptionConstant.CanNotFoundEntityException)
        {
        }
        public CanNotFoundEntityException(string predicate) : base(predicate, ExceptionConstant.CanNotFoundEntityException)
        {
        }
    }
    public class ThereIsNoEntityWithCurrentPredicate : EntityException
    {
        public ThereIsNoEntityWithCurrentPredicate(string predicate) : base(-1, predicate, ExceptionConstant.ThereIsNoEntityWithCurrentPredicate)
        {
        }
    }


    public class CurrentEntityAlreadyExistException : EntityException
    {
        public CurrentEntityAlreadyExistException(string entity) : base(-1, entity, ExceptionConstant.CurrentEntityAlreadyExist)
        {
        }
    }

    public class CurrentUsernameAndPasswordAlreadyExistException : EntityException
    {
        public CurrentUsernameAndPasswordAlreadyExistException(string username, string password) : base(-1, $"{username}:{password}", ExceptionConstant.CurrentUsernameAndPasswordAlreadyExist)
        {
        }

    }



}
