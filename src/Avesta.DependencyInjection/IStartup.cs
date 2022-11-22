using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.DependencyInjection
{

    public interface IStartup<T> where T : class
    {
        T RegisterModules(IServiceCollection services);
        T RegisterServices(IServiceCollection services);
        T RegisterMiddlewares();
        T OnApplicationStart();
    }
}
