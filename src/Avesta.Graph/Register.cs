using Avesta.Graph.Service;
using Microsoft.Extensions.DependencyInjection;

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
