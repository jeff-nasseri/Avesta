using Avesta.Share.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Seed.Model
{

    public class SeedResultModel : BaseModel
    {
        public SeedResultModel() : this(false)
        {
        }
        public SeedResultModel(bool successful) : this(successful, string.Empty)
        {
        }
        public SeedResultModel(bool successful, string? message)
        {
            Message = message;
            Successful = successful;
        }


        public string? Message { get; set; }
        public bool Successful { get; set; }
    }
}
