using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Storage.Seed
{
    public abstract class SeedStorage
    {
        public abstract string Alphabet { get; }
        public abstract string EmailExtension { get; }
        public abstract int Numbers { get; }
        public abstract string NonAlphabet { get; }

        public abstract List<string> PhoneNumberPrefix { get; }
        public abstract List<string> Countries { get; }
        public abstract List<string> Names { get; }

        public abstract List<char> AlphabetList { get; }
        public abstract List<char> NonAlphabetList { get; }
        public abstract List<int> NumbersList { get; }

        public abstract List<(string FirstName, string LastName)> TupleNames { get; }
    }
}
