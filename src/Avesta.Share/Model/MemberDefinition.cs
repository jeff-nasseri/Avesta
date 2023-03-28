using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model
{
    public class MemberDefinition
    {
        static string[] NumberDefinitionKeys = { "number" };
        static string[] EmailDefinitionKeys = { "email" };
        static string[] NameDefinitionKeys = { "name" };
        static string[] FirstNameDefinitionKeys = { "firstname" };
        static string[] LastNameDefinitionKeys = { "lastname" };
        static string[] FullNameDefinitionKeys = { "fullname" };
        static string[] CountryDefinitionKeys = { "country", "region" };
        static string[] PhoneDefinitionKeys = { "phone", "phonenumber" };


        public static Definition GetDefinition(string memberName)
        {
            var result = memberName.ToLower().Trim() switch
            {
                string number when NumberDefinitionKeys.Any(number.Contains) => Definition.Number,
                string email when EmailDefinitionKeys.Any(email.Contains) => Definition.Email,
                string name when NameDefinitionKeys.Any(name.Contains) => Definition.Name,
                string firstName when FirstNameDefinitionKeys.Any(firstName.Contains) => Definition.FirstName,
                string lastName when LastNameDefinitionKeys.Any(lastName.Contains) => Definition.LastName,
                string fullName when FullNameDefinitionKeys.Any(fullName.Contains) => Definition.FullName,
                string country when CountryDefinitionKeys.Any(country.Contains) => Definition.Country,
                string phone when PhoneDefinitionKeys.Any(phone.Contains) => Definition.PhoneNumber,
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
            Number,
            NotFound
        }

    }
}
