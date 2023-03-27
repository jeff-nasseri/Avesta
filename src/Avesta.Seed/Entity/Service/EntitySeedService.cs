using Avesta.Data.Context;
using Avesta.Data.Model;
using Avesta.Repository.EntityRepository;
using Avesta.Repository.EntityRepositoryRepository;
using Avesta.Repository.Identity;
using Avesta.Services;
using Avesta.Share.Extensions;
using Avesta.Share.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed.Entity.Service
{

    public interface IEntitySeedService
    {
        Task Seed<TEntity>(TEntity entity) where TEntity : BaseEntity<string>;
        Task SeedRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<string>;
    }


    //public class EntitySeedService<TContext> : IEntitySeedService
    //    where TContext : AvestaDbContext
    //{
    //    readonly EntityRepository<TContext> _entityRepository;
    //    public EntitySeedService(EntityRepository<TContext> entityRepository)
    //    {
    //        _entityRepository = entityRepository;
    //    }



    //    public async Task Seed<TEntity>(TEntity entity) where TEntity : BaseEntity<string>
    //    {
    //        await _entityRepository.InsertAsync(entity);
    //    }



    //    public async Task SeedRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<string>
    //    {
    //        await _entityRepository.InsertRange(entities);
    //    }



    //}



    public interface IEntitySeedService<TId, TEntity, TAvestaDbContext>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TAvestaDbContext : AvestaDbContext
    {
        Task<SeedResultModel> SeedEntity(int number = 100);
    }

    public interface IDbSeedService<TAvestaDbContext>
        where TAvestaDbContext : AvestaDbContext
    {

    }




    public class EntitySeedService<TId, TEntity, TAvestaDbContext> : IEntitySeedService<TId, TEntity, TAvestaDbContext>
        where TId : class
        where TEntity : BaseEntity<TId>
        where TAvestaDbContext : AvestaDbContext
    {
        readonly IEntityRepository<TEntity, TId> _entityRepository;
        readonly ISeedDataGenerator _dataGenerator;
        public EntitySeedService(IEntityRepository<TEntity, TId> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public async Task<SeedResultModel> SeedEntity(int number = 100)
        {
            number.ForEach(Extension.Catcher(() =>
            {
                var instance = CreateInstance<TEntity>(_dataGenerator);
                _entityRepository.Insert(instance);
            }, (e) => Console.WriteLine(e.Message)));


            throw new NotImplementedException();
        }


        public T CreateInstance<T>(ISeedDataGenerator generator)
        {
            var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            var obj = Activator.CreateInstance<T>();

            foreach (var prop in props)
            {
                var value = prop.GenerateValue(generator);
                prop.SetValue(obj, value);
            }

            return obj;
        }




    }


    public static class Extension
    {
        public static object GenerateValue(this PropertyInfo info, ISeedDataGenerator generator)
        {
            var definition = MemberDefinition.GetDefinition(info.Name);
            var value = generator.GenerateValueByDefinition(definition);
            return value;
        }





    }




    public class Try
    {

        //TODO : use logger for built in log in Try and dont forget to consider log severity as well

        readonly ILogger _logger;

        public Try(ILogger logger)
        {
            _logger = logger;
        }



        public static Action Catcher(Action tryAction, Action<Exception>? catchAction = null)
              => Catcher<Exception>(tryAction, catchAction);


        public static Action Catcher<TException>(Action tryAction, Action<TException>? catchAction = null)
            where TException : Exception
            => Catcher<TException>(tryAction, catchAction);


        public static Action Catcher<TException>(Action tryAction, Action<TException>? catchAction = null, Action? final = null)
            where TException : Exception
            => delegate
            {
                try
                {
                    tryAction();
                }
                catch (TException exception)
                {
                    catchAction?.Invoke(exception);
                }
                finally
                {
                    final?.Invoke();
                }
            };
    }




















    public interface ISeedDataGenerator
    {
        string GeneratePhoneNumber();
        string GenerateFullName();
        string GenerateEmail();
        string GenerateFirstName();
        string GenerateLastName();
        string GenerateRandomString();
        string GenerateRandomNumber();
        string GenerateCountryName();
        T GenerateDefaultValue<T>();
        string GenerateValueByDefinition(MemberDefinition.Definition definition);
    }

    public class MemberDefinition
    {
        const string EmailDefinition = "email";
        const string NameDefinition = "name";
        const string FirstNameDefinition = "firstname";
        const string LastNameDefinition = "lastname";
        const string FullNameDefinition = "fullname";
        const string CountryDefinition = "country;region";
        const string phoneDefinition = "phone;phonenumber";


        public static Definition GetDefinition(string memberName)
        {
            var result = memberName.ToLower().Trim() switch
            {
                //TODO : should be complete
                string email when email.Contains(EmailDefinition) => Definition.Email,
                string b when b.Contains("test2") => Definition.Email,
                _ => Definition.NotFound
            };

            return result;
        }


        public enum Definition : int
        {
            PhoneNumber,
            Email,
            FirstName,
            LastName,
            FullName,
            Name,
            Country,
            NationalCode,
            NotFound
        }

    }



    public static class SeedStorage
    {
        public const string Alphabet = "qwertyuioplkjhgfdsazxcvbnm";
        public const string EmailExtension = "@email.com";
        public const int Numbers = 1234567890;
        public const string NonAlphabet = @"!@#$%^&*()_+}{]["":';?></.,";


        public static List<string> PhoneNumberPrefix = new List<string>() { "+98", "+89", "84", "32", "34", "+45", "+81", "+686", "+383", "+965", "+996", "+856", "+60" };
        public static List<string> Countries = new List<string>() { "KE", "KZ", "JO", "JE", "IQ", "ID", "HU", "HK", "HN", "HT", "GW", "US" };
        public static List<string> Names = new List<string>() { "abbott", "acosta", "adams", "adkins", "aguilar"
            , "abby", "abigail", "adele", "adrian", "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };



        public static List<char> AlphabetList = Alphabet.ToCharArray().ToList();
        public static List<char> NonAlphabetList = NonAlphabet.ToCharArray().ToList();
        public static List<int> NumbersList = Numbers.ToString().Select(n => int.Parse(n.ToString())).ToList();
        public static List<(string FirstName, string LastName)> TupleNames = Names.JoinEach((n1, n2) => (n1, n2)).ToList();

    }




    public class SeedResultModel : BaseModel
    {
        public string? Message { get; set; }
        public bool Successful { get; set; }
    }






}
