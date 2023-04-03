using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Graph.Model
{
    public class EntityInformation : BasicInformation
    {

        public EntityInformation(params PropertyInformation[] data) : this(data?.ToList())
        {
        }

        public EntityInformation(IEnumerable<PropertyInformation>? properties) : this()
        {
            Properties = properties;
        }

        public EntityInformation() : base()
        {
        }


        public IEnumerable<PropertyInformation>? Properties { get; set; }
    }
}
