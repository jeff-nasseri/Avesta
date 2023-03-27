using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model
{
    public class BaseModel : BaseModel<string>
    {
    }

    public class BaseModel<T> : Model
        where T : class
    {
        /// <summary>
        /// current model or entity id with type of 'T'
        /// </summary>
        public virtual T? ID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public bool IsLock { get; set; } = false;


        public override string ToString()
        {
            var result = JsonConvert.SerializeObject(this);
            return result;
        }
    }


    public abstract class Model
    {
    }
  
    
}
