using Avesta.Seed.Entity.Service;
using Avesta.Share.Extensions;
using Avesta.Share.Model;
using Avesta.Storage.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed
{
    public class AvestaSeedDataGenerator : ISeedDataGenerator
    {

        readonly SeedStorage _storage;
        public AvestaSeedDataGenerator(SeedStorage storage)
        {
            _storage = storage;
        }


        public string GenerateCountryName()
        {
            var result = _storage.Countries.RandomChoose();
            return result;
        }

        public T GenerateDefaultValue<T>()
        {
            var result = Activator.CreateInstance<T>();
            return result;
        }

        public string GenerateEmail()
        {
            var name = _storage.Names.RandomChoose();
            return $"{name}{_storage.EmailExtension}";
        }

        public string GenerateFirstName()
        {
            var result = _storage.TupleNames.RandomChoose().FirstName;
            return result;
        }

        public string GenerateFullName()
        {
            var tuple = _storage.TupleNames.RandomChoose();
            return $"{tuple.FirstName} {tuple.LastName}";
        }

        public string GenerateLastName()
        {
            var result = _storage.TupleNames.RandomChoose().LastName;
            return result;
        }

        public string GeneratePhoneNumber()
        {
            var prefix = _storage.PhoneNumberPrefix.RandomChoose();
            var number = (new Random()).Next(1234567890, 1876543210);
            return $"{prefix}{number}";
        }

        public long GenerateRandomNumber(int length = 5)
        {
            var result = (new Random()).NextInt64(Convert.ToInt64(Math.Pow(10, length - 1)));
            return result;
        }
        public string GenerateRandomString(int length = 5)
        {
            var result = string.Empty;

            length.For(() =>
            {
                var str = _storage.Alphabet.RandomChoose().ToString();
                result += str;
            });

            return result;
        }

        public T GenerateValueByDefinition<T>(MemberDefinition.Definition definition)
        {
            switch (definition)
            {
                case MemberDefinition.Definition.Number: return (T)Convert.ChangeType(GenerateFirstName(), typeof(T));
                case MemberDefinition.Definition.Name: return (T)Convert.ChangeType(GenerateFirstName(), typeof(T));
                case MemberDefinition.Definition.FirstName: return (T)Convert.ChangeType(GenerateFirstName(), typeof(T));
                case MemberDefinition.Definition.LastName: return (T)Convert.ChangeType(GenerateLastName(), typeof(T));
                case MemberDefinition.Definition.FullName: return (T)Convert.ChangeType(GenerateFullName(), typeof(T));
                case MemberDefinition.Definition.PhoneNumber: return (T)Convert.ChangeType(GeneratePhoneNumber(), typeof(T));
                case MemberDefinition.Definition.Country: return (T)Convert.ChangeType(GenerateCountryName(), typeof(T));
                case MemberDefinition.Definition.NationalCode: return (T)Convert.ChangeType(GenerateRandomNumber(8), typeof(T));
                default: return GenerateDefaultValue<T>();
            }
        }
    }

}
