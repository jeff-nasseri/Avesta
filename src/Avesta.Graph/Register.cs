using Avesta.Graph.Service;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Avesta.Graph
{
    public static class Register
    {
        public static IServiceCollection RegisterAvestaGraph(this IServiceCollection services)
        {
            services.AddScoped<IGraphHierarchySrevice, GraphHierarchySrevice>();
            return services;
        }
    }
}
