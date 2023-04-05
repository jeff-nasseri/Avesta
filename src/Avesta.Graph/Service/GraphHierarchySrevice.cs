using Avesta.Data.Model;
using Avesta.Graph.Controller;
using Avesta.Graph.Model;
using Avesta.Share.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Service
{

    public interface IGraphHierarchySrevice
    {
        Task<DataHierarchy> GetHierarchyOfCurrentExecuteableApplication();
    }

    public class GraphHierarchySrevice : IGraphHierarchySrevice
    {

        public async Task<DataHierarchy> GetHierarchyOfCurrentExecuteableApplication()
        {
            await Task.CompletedTask;

            var entities = new List<EntityInformation>();

            var types = Assembly.GetEntryAssembly().GetAllDrivenTypes<BaseEntity>();

            foreach (var type in types)
            {

                if (entities.Any(e => e.FullName == type.FullName))
                    continue;


                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)?.Select(p => new PropertyInformation
                {
                    Name = p.Name,
                    Type = p.PropertyType.ToString(),
                    FullName = p.Name
                }).ToList();

                var entity = new EntityInformation(properties)
                {
                    Name = type.Name,
                    FullName = type.FullName,
                    Type = type.ToString()
                };

                entities.Add(entity);
            }


            var result = new DataHierarchy(entities);
            return result;

        }
    }

}
