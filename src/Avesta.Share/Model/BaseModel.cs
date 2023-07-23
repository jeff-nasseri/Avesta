using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avesta.Share.Model
{
    public class BaseModel : BaseModel<string>
    {
        public BaseModel() : base(Guid.NewGuid().ToString())
        {
        }

        public BaseModel(DateTime createdDate) : base(createdDate)
        {
        }
    }

    public class BaseModel<T> : Model
        where T : class
    {

        public BaseModel()
        {
        }
        public BaseModel(DateTime createdDate)
        {
            CreatedDate = createdDate;
        }
        public BaseModel(T? id) : this(DateTime.UtcNow)
        {
            ID = id;
        }


        /// <summary>
        /// current model or entity id with type of 'T'
        /// </summary>
        public virtual T? ID { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public virtual bool IsLock { get; set; } = false;


        public override string ToString()
        {
            var result = JsonConvert.SerializeObject(this);
            return result;
        }
    }


    public abstract class Model
    {
    }

    public abstract class BaseViewModel : BaseModel
    {
        public virtual DateTime? DeletedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }

    }

}
