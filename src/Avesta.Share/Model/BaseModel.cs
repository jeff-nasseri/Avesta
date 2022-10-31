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
    {
        /// <summary>
        /// current model or entity id with type of 'T'
        /// </summary>
        public virtual T? ID { get; set; }
    }


    public abstract class Model
    {
    }
  
    
}
