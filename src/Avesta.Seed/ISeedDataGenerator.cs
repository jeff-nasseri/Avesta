using Avesta.Seed.Entity.Service;
using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed
{
    public interface ISeedDataGenerator
    {
        string GeneratePhoneNumber();
        string GenerateFullName();
        string GenerateEmail();
        string GenerateFirstName();
        string GenerateLastName();
        string GenerateRandomString(int length = 5);
        long GenerateRandomNumber(int length = 5);
        string GenerateCountryName();
        T GenerateDefaultValue<T>();
        T GenerateValueByDefinition<T>(MemberDefinition.Definition definition);
    }

}
