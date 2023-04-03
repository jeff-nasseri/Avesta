using Avesta.Graph.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Model
{
    public class DataHierarchy : BasicInformation
    {

        public DataHierarchy(params EntityInformation[] data) : this(data?.ToList())
        {
        }

        public DataHierarchy(IEnumerable<EntityInformation>? entities) : this()
        {
            Entities = entities;
        }

        public DataHierarchy() : base()
        {
        }

        public IEnumerable<EntityInformation>? Entities { get; set; }

    }
}
