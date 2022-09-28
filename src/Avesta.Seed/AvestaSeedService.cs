using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed
{
    public interface IAvestaSeedService
    {
        Task SeedAll(int n);
    }
}
