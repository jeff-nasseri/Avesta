using Avesta.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Storage.Seed
{
    public class AvestaSeedStorage : SeedStorage
    {

        public override string Alphabet => "qwertyuioplkjhgfdsazxcvbnm";
        public override string EmailExtension => "@email.com";
        public override int Numbers => 1234567890;
        public override string NonAlphabet => @"!@#$%^&*()_+}{]["":';?></.,";


        public override List<string> PhoneNumberPrefix => new List<string>() { "+98", "+89", "84", "32", "34", "+45", "+81", "+686", "+383", "+965", "+996", "+856", "+60" };
        public override List<string> Countries => new List<string>() { "KE", "KZ", "JO", "JE", "IQ", "ID", "HU", "HK", "HN", "HT", "GW", "US" };
        public override List<string> Names => new List<string>() { "abbott", "acosta", "adams", "adkins", "aguilar"
            , "abby", "abigail", "adele", "adrian", "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };



        public override List<char> AlphabetList => Alphabet.ToCharArray().ToList();
        public override List<char> NonAlphabetList => NonAlphabet.ToCharArray().ToList();
        public override List<int> NumbersList => Numbers.ToString().Select(n => int.Parse(n.ToString())).ToList();



        public override List<(string FirstName, string LastName)> TupleNames => Names.JoinEach((n1, n2) => (n1, n2)).ToList();

    }

}
